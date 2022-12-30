using Fluxor;
using System.Collections.Generic;
using WebBlazor.Models;

namespace WebBlazor.Store.Account
{
    public record AccountState
    {
        public List<MyAccountModel> AccountList { get; set; }
    }

    public class CountorFeatureState : Feature<AccountState>
    {
        public override string GetName() => nameof(AccountState);

        protected override AccountState GetInitialState()
        {
            return new AccountState
            {
                AccountList = null
            };
        }
    }
}
