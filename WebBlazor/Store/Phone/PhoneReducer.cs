using Fluxor;

namespace WebBlazor.Store.Phone
{
    public static class PhoneReducer
    {
        [ReducerMethod]
        public static PhoneState OnAddPhone(PhoneState state, SetPhone action)
        {
            return state with
            {
                PhoneList = action.phoneList
            };
        }
    }
}
