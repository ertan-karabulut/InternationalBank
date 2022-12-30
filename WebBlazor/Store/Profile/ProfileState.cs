using Fluxor;
using WebBlazor.Models;

namespace WebBlazor.Store.Profile
{
    public record ProfileState
    {
        public ClaimModel Claim { get; set; }
    }

    public class ProfileFeatureState : Feature<ProfileState>
    {
        public override string GetName() => nameof(ProfileState);

        protected override ProfileState GetInitialState()
        {
            return new ProfileState
            {
                Claim = null
            };
        }
    }
}
