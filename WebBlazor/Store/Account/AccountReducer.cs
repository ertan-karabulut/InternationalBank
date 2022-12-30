using Fluxor;

namespace WebBlazor.Store.Account
{
    public static class AccountReducer
    {
        [ReducerMethod]
        public static AccountState OnAddAccount(AccountState state, SetAccount action)
        {
            return state with
            {
                AccountList = action.accountList
            };
        }
    }
}
