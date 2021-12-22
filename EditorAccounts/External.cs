using System.Collections.Generic;

namespace EditorAccounts
{
    static class External
    {
        public static Structs.sITEMLIST g_pItemList = new Structs.sITEMLIST();
        public static Structs.STRUCT_ACCOUNTFILE g_pAcccount = new Structs.STRUCT_ACCOUNTFILE();
        public static List<Structs.STRUCT_ACCOUNTFILE> g_pContas = new List<Structs.STRUCT_ACCOUNTFILE>();
        public static List<Structs.STRUCT_ACCOUNTFILE> ContasLocalizadas = new List<Structs.STRUCT_ACCOUNTFILE>();
        public static int CurrentChar = -1;
    }
}
