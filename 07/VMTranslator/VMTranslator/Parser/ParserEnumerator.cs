using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace VMTranslator
{
    class ParserEnumerator : IEnumerator<Command>
    {
        private StreamReader file;
        private Command current;

        public ParserEnumerator(System.IO.StreamReader file)
        {
            this.file = file;
        }

        public Command Current
        {
            get { return current; }
        }

        object IEnumerator.Current
        {
            get { return this.current; }
        }

        public bool MoveNext()
        {
            do
            {
                var line = file.ReadLine();
                if (line != null)
                {
                    var command = RemoveComments(line);
                    var commandParts = command.Split(' ');
                    switch (commandParts[0].ToLower())
                    {
                        case "add":
                            current = new AddCommand();
                            return true;

                        case "sub":
                            current = new SubCommand();
                            return true;

                        case "and":
                            current = new AndCommand();
                            return true;

                        case "or":
                            current = new OrCommand();
                            return true;

                        case "neg":
                            current = new NegateCommand();
                            return true;

                        case "not":
                            current = new NotCommand();
                            return true;

                        case "eq":
                            current = new EqCommand();
                            return true;

                        case "lt":
                            current = new LtCommand();
                            return true;

                        case "gt":
                            current = new GtCommand();
                            return true;

                        case "push":
                            switch (commandParts[1].ToLower())
                            {
                                case "constant":
                                    current = new PushConstant(commandParts[2]);
                                    return true;
                                case "local":
                                    current = new PushLocal(commandParts[2]);
                                    return true;
                                case "argument":
                                    current = new PushArgument(commandParts[2]);
                                    return true;
                                case "this":
                                    current = new PushThis(commandParts[2]);
                                    return true;
                                case "that":
                                    current = new PushThat(commandParts[2]);
                                    return true;
                                case "temp":
                                    current = new PushTemp(commandParts[2]);
                                    return true;
                                case "pointer":
                                    current = new PushPointer(commandParts[2]);
                                    return true;
                                case "static":
                                    current = new PushStatic(FileName, commandParts[2]);
                                    return true;
                            }
                            break;
                        case "pop":
                            switch (commandParts[1].ToLower())
                            {
                                case "local":
                                    current = new PopLocal(commandParts[2]);
                                    return true;
                                case "argument":
                                    current = new PopArgument(commandParts[2]);
                                    return true;
                                case "this":
                                    current = new PopThis(commandParts[2]);
                                    return true;
                                case "that":
                                    current = new PopThat(commandParts[2]);
                                    return true;
                                case "temp":
                                    current = new PopTemp(commandParts[2]);
                                    return true;
                                case "pointer":
                                    current = new PopPointer(commandParts[2]);
                                    return true;
                                case "static":
                                    current = new PopStatic(FileName, commandParts[2]);
                                    return true;
                            }
                            break;
                        case "label":
                            current = new Label(commandParts[1]);
                            return true;
                        case "if-goto":
                            current = new IfGoto(commandParts[1]);
                            return true;
                        case "goto":
                            current = new Goto(commandParts[1]);
                            return true;
                        case "function":
                            current = new Function(commandParts[1], commandParts[2]);
                            return true;
                        case "call":
                            current = new Call(commandParts[1], commandParts[2]);
                            return true;
                        case "return":
                            current = new Return();
                            return true;
                    }
                }
            } while (!file.EndOfStream);

            return false;
        }

        public void Reset()
        {
            file.BaseStream.Position = 0;
            file.DiscardBufferedData();
        }

        public void Dispose()
        {
            // Do nothing
        }

        private string RemoveComments(string line)
        {
            var lineParts = line.Split(new string[] { "//" }, System.StringSplitOptions.None);
            return lineParts[0].Trim();
        }

        public string FileName
        {
            get
            {
                var fileInfo = new FileInfo(((FileStream)file.BaseStream).Name);
                return fileInfo.Name;
            }
        }
    }
}
