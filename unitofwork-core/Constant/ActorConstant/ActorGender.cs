namespace unitofwork_core.Constant.ActorConstant
{
    public static class ActorGender
    {
        public const string MALE = "MALE";
        public const string FEMALE = "FEMALE";
        public const string OTHER = "OTHER";

        public static List<string> GetAll() { 
            List<string> strings = new List<string> { 
                MALE,FEMALE,OTHER
            };
            return strings;
        }
    }
}
