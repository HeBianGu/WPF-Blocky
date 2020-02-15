﻿using ScratchNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoScriptEditor
{
    public delegate void OutWriteLine(object obj, params object[] param);
    public class LogStatement : Statement, Execution2
    {
        public LogStatement()
        {
            Message = new Literal() { Raw = "Log Text" };
        }
        public Expression Message { get; set; }
        public string ReturnType
        {
            get { return "void"; }
        }
        public Completion Execute(ExecutionEnvironment enviroment)
        {
            return Completion.Void;
        }

        public Descriptor Descriptor
        {
            get
            {
                Descriptor desc = new Descriptor();
                desc.Add(new TextItemDescriptor(this, "Console.WriteLine("));
                desc.Add(new ExpressionDescriptor(this, "Message", "string|number|boolean") { IsOnlyNumberAllowed = false });
                desc.Add(new TextItemDescriptor(this, ")"));
                return desc;
            }
        }
        public string Type
        {
            get
            {
                return "MoveStatement";
            }
        }


        public BlockDescriptor BlockDescriptor
        {
            get { return null; }
        }


        public bool IsClosing
        {
            get { return false; }
        }

        public static OutWriteLine WriteLine { get; set; }
        //execution
        object logValue = null;

        public ExecutionEnvironment StartCall(ExecutionEnvironment e)
        {
            return e;
        }

        public Completion EndCall(ExecutionEnvironment e)
        {
            Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " " + logValue);
            if (WriteLine != null)
            {
                WriteLine(logValue);
            }
            return Completion.Void;
        }

        public bool PopStack(out object execution, out ExecutionCallback callback, ExecutionEnvironment e)
        {
            execution = Message;
            callback = Callback;
            return false;
        }
        Nullable<DateTime> Callback(object value, object exception, ExecutionEnvironment e)
        {
            logValue = value;
            return null;
        }
        public bool HandleException(object exception)
        {
            throw new NotImplementedException();
        }
    }
}
