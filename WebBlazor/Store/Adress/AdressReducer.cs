using Fluxor;

namespace WebBlazor.Store.Account
{
    public static class AdressReducer
    {
        [ReducerMethod]
        public static AdressState OnAddAdress(AdressState state, SeAdress action)
        {
            return state with
            {
                AdressList = action.adressList
            };
        }
    }
}
