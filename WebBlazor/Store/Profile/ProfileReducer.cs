using Fluxor;

namespace WebBlazor.Store.Profile
{
    public static class ProfileReducer
    {
        [ReducerMethod]
        public static ProfileState OnAddProfile(ProfileState state, SetProfile action)
        {
            return state with
            {
                Claim = action.claim
            };
        }
    }
}
