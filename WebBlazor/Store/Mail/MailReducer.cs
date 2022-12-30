using Fluxor;

namespace WebBlazor.Store.Mail
{
    public static class MilReducer
    {
        [ReducerMethod]
        public static MailState OnAddMail(MailState state, SeTMail action)
        {
            return state with
            {
                MailList = action.mailList
            };
        }
    }
}
