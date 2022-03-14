namespace Lexico2
{
    public class Lexico : Token
    {
         StreamReader archivo;
        StreamWriter Log;
        const int f = -1;
        const int e = -2;

        public Lexico()
        {
            archivo = new StreamReader("C:\\Users\\marco\\OneDrive\\Escritorio\\Lenguajes y automatas 1\\Lexico2\\prueba.cpp");
            Log     = new StreamWriter("C:\\Users\\marco\\OneDrive\\Escritorio\\Lenguajes y automatas 1\\Lexico2\\prueba.Log"); 
            Log.AutoFlush = true;
        }
        public void cerrar()
        {
            archivo.Close();
            Log.Close();
        }       

        private void clasifica(int estado)
        {
            switch(estado)
            {
                case 1:
                    setClasificacion(Tipos.Identificador);
                    break;
                case 2:
                    setClasificacion(Tipos.Numero);
                    break;
                case 8:
                    setClasificacion(Tipos.Asignacion);
                    break;
                case 9:
                case 17:
                case 18:
                case 19:
                    setClasificacion(Tipos.OperadorRelacional);
                    break;
                case 10:
                case 13:
                case 14:
                    setClasificacion(Tipos.Caracter);
                    break;
                case 11:
                    setClasificacion(Tipos.Inicializacion);
                    break;
                case 12:
                    setClasificacion(Tipos.FinSentencia);
                    break;
                case 15:
                case 16:
                    setClasificacion(Tipos.OperadorLogico);
                    break;
                case 21:
                case 22:
                    setClasificacion(Tipos.OperadorTermino);
                    break;
                case 23:
                    setClasificacion(Tipos.IncrementoTermino);
                    break;   
                case 24:
                case 29:
                    setClasificacion(Tipos.OperadorFactor);
                    break;
                case 25:
                    setClasificacion(Tipos.IncrementoFactor);
                    break;
                case 26:
                    setClasificacion(Tipos.Cadena);
                    break;
                case 28:
                    setClasificacion(Tipos.OperadorTernario);
                    break;
                case 33:
                    setClasificacion(Tipos.Caracter);
                    break;
            }
        }

        public void NextToken() 
        {
            string buffer = "";           
            char c;      
            int estado = 0;


            while(estado >= 0)
            {
                c = (char)archivo.Peek(); //Funcion de transicion
                estado = automata(estado,c);
                clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    if (estado >0)
                    {
                        buffer += c;
                    }
                }
            }
            setContenido(buffer); 
            Log.WriteLine(getContenido() + " | " + getClasificacion());
        }

        private int automata(int estado,char t)
        {
            switch(estado)
                {
                    case 0:

                        if (char.IsLetter(t))
                        {
                            estado = 1;
                        }
                        else if (char.IsDigit(t))
                        {
                            estado = 2;
                        }
                        else if (t == '=')
                        {
                            estado = 8;
                        }
                        else if (t == ':')
                        {
                            estado = 10;
                        }
                        break;
                    case 1:
                        if (!char.IsLetterOrDigit(t))
                        {
                            estado = f;
                        }
                        break;
                    case 2:
                        if (t == '.')
                        {
                            estado = 3;
                        }
                        else if (t == 'e' || t == 'E')
                        {
                            estado = 5;
                        }
                        else if (!char.IsDigit(t))
                        {
                            estado = f;
                        }
                        break;
                    case 3:
                        if (char.IsDigit(t))
                        {
                            estado = 4;
                        }
                        else
                        {
                            estado = e;
                        }
                        break;
                    case 4:
                        if (t == 'e' || t == 'E')
                        {
                            estado = 5;
                        }
                        else if (!char.IsDigit(t))
                        {
                            estado = f;
                        }

                        break;
                    case 5:
                        if (t == '+' || t == '-')
                        {
                            estado = 6;
                        }
                        else if (char.IsDigit(t))
                        {
                            estado = 7;
                        }
                        else
                        {
                            estado = e;
                        }
                        break;
                    case 6:
                        if (char.IsDigit(t))
                        {
                            estado = 7;
                        }
                        else
                        {
                            estado = e;
                        }
                        break;
                    case 7:
                        if (!char.IsDigit(t))
                        {
                            estado = f;
                        }
                        break;
                    case 8:
                        if (t == '=')
                        {
                            estado = 9;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 9:
                    case 11:
                        estado = f;
                        break;
                    case 10:
                        if (t == '=')
                        {
                            estado = 11;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        break;
                    case 17:
                        break;
                    case 18:
                        break;
                    case 19:
                        break;
                    case 20:
                        break;
                    case 21:
                        break;
                    case 22:
                        break;
                    case 23:
                        break;
                    case 24:
                        break;
                    case 25:
                        break;
                    case 26:
                        break;
                    case 27:
                        break;
                    case 28:
                        break;
                    case 29:
                        break;
                    case 30:
                        break;
                    case 31:
                        break;
                    case 32:
                        break;
                    case 33:
                        break;
                }
            return estado;
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}