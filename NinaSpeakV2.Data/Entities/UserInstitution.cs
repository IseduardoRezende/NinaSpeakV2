﻿namespace NinaSpeakV2.Data.Entities
{
    public class UserInstitution : BaseEntity
    {
        public UserInstitution()
        {
            ChatBotUserInstitutions = new List<ChatBotUserInstitution>();
        }

        public long UserFk { get; set; }

        public long InstitutionFk { get; set; }

        public bool Owner { get; set; }

        public bool Creator { get; set; }

        public User User { get; set; }

        public Institution Institution { get; set; }
    
        public ICollection<ChatBotUserInstitution> ChatBotUserInstitutions { get; set; }
    }
}
