﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class General
    {
        public static class ProcessType
        {
            public static int Login = 1;
            public static int AddressAdd = 2;
            public static int AddressUpdate = 3;
            public static int AddressDelete = 4;
            public static int AdsAdd = 5;
            public static int AdsUpdate = 6;
            public static int AdsDelete = 7;
            public static int CategoryAdd = 8;
            public static int CategoryUpdate = 9;
            public static int CategoryDelete = 10;
            public static int IconAdd = 11;
            public static int IconUpdate = 12;
            public static int IconDelete = 13;
            public static int MetaAdd = 14;
            public static int MetaUpdate = 15;
            public static int MetaDelete = 16;
            public static int SocialAdd = 17;
            public static int SocialUpdate = 18;
            public static int SocialDelete = 19;
            public static int UserAdd = 20;
            public static int UserUpdate = 21;
            public static int UserDelete = 22;
            public static int VideoAdd = 23;
            public static int VideoUpdate = 24;
            public static int VideoDelete = 25;
            public static int PostAdd = 26;
            public static int PostUpdate = 27;
            public static int PostDelete = 28;
            public static int ImageAdd = 29;
            public static int ImageUpdate = 30;
            public static int ImageDelete = 31;
            public static int TagAdd = 32;
            public static int TagUpdate = 33;
            public static int TagDelete = 34;
            public static int CommentApprove = 35;
            public static int CommentDelete = 36;
            public static int ContactRead = 37;
            public static int ContactDelete = 38;
        }

        public static class TableName
        {
            public static int Login = 41;
            public static int Address = 42;
            public static int Ads = 43;
            public static int Category = 44;
            public static int Icon = 45;
            public static int Meta = 46;
            public static int Social = 47;
            public static int User = 48;
            public static int Video = 49;
            public static int Post = 50;
            public static int Image = 51;
            public static int Tag = 52;
            public static int Comment = 53;
            public static int Contact = 54;
        }

        public static class Messages
        {
            public static int AddSuccess = 1;
            public static int EmptyArea = 2;
            public static int UpdateSuccess = 3;
            public static int ImageMissing = 4;
            public static int ExtensionError = 5;
            public static int GeneralError = 6;
        }
    }
}