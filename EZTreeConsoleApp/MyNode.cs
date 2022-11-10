using db.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace db.EZTreeConsoleApp
{
    public class MyNode : Node
    {
        public MyNode(object id) : base(id)
        {
        }

        public MyNode(object id, string text, string imagePath, string a_attr) : this(id)
        {
            Text = text;
            ImagePath = imagePath;
            A_attr = a_attr;
        }

        public string Text { get; set; }
        public string ImagePath { get; set; }
        public string A_attr { get; set; }
    }
}
