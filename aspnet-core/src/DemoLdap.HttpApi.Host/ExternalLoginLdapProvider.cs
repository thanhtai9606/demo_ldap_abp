using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Identity;
using Novell.Directory.Ldap;

namespace Becamex.ArchiveCenter
{
    public class ExternalLoginLdapProvider : ExternalLoginProviderBase, ITransientDependency
    {
        public const string Name = "Ldap";
        private readonly LdapConnection _connection;
        private readonly IConfiguration _configuration;
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private CurrentUserInfo _currentUserInfo = new CurrentUserInfo();
        public ExternalLoginLdapProvider(IGuidGenerator guidGenerator, ICurrentTenant currentTenant, IdentityUserManager userManager, IIdentityUserRepository identityUserRepository, IOptions<IdentityOptions> identityOptions, IConfiguration configuration)
          : base(guidGenerator, currentTenant, userManager, identityUserRepository, identityOptions)
        {
            _configuration = configuration;
            _connection = new LdapConnection
            {
                SecureSocketLayer = bool.Parse(_configuration["LDAP:ssl"])
            };

        }

        public async override Task<bool> TryAuthenticateAsync(string userName, string plainPassword)
        {
            bool result = false;

            string user_dn = string.Format("becamex\\{0}", userName);
            _connection.Connect(_configuration["LDAP:host"], int.Parse(_configuration["LDAP:port"]));
            _connection.Bind(user_dn, plainPassword);
            result = _connection.Bound;
            if (result)
                _currentUserInfo = GetPersonalInfo(userName, plainPassword);
            _connection.Dispose();
            return await Task.FromResult(result);
        }

        protected override Task<ExternalLoginUserInfo> GetUserInfoAsync(string userName)
        {

            return Task.FromResult(
                new ExternalLoginUserInfo(_currentUserInfo.Username + "@becamex.com.vn")
                {
                    Name = _currentUserInfo.DisplayName, //optional, if the provider knows it
                    Surname = "", //optional, if the provider knows it
                    EmailConfirmed = true, //optional, if the provider knows it
                    TwoFactorEnabled = false, //optional, if the provider knows it
                    PhoneNumber = "0000-000-000", //optional, if the provider knows it
                    PhoneNumberConfirmed = false, //optional, if the provider knows it
                    ProviderKey = "123" //The id of the user on the provider side
                }
              );
        }
        private CurrentUserInfo GetPersonalInfo(string userName, string plainPassword)
        {
            var searchFilter = string.Format(_configuration["LDAP:search_filter"], userName);
            var result = _connection.Search(
                _configuration["LDAP:base_dn"],
                LdapConnection.ScopeSub,
                searchFilter,
                new[] { MemberOfAttribute, DisplayNameAttribute, SAMAccountNameAttribute },
                false
            );
            var user = result.Next();
            if (user != null)

                _currentUserInfo = new CurrentUserInfo
                {
                    DisplayName = user.GetAttribute(DisplayNameAttribute).StringValue,
                    Username = user.GetAttribute(SAMAccountNameAttribute).StringValue
                };
            _connection.Disconnect();
            return _currentUserInfo;
        }
    }
    public class CurrentUserInfo
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
}



