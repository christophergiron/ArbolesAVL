namespace ArbolesAVL
{
    using System;

    class Nodo
    {
        public int Valor;
        public Nodo Izq, Der;
        public int Altura;

        public Nodo(int valor)
        {
            Valor = valor;
            Altura = 1;
        }
    }
    class ABB
    {
        public Nodo Insertar(Nodo nodo, int valor)
        {
            if (nodo == null)
                return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izq = Insertar(nodo.Izq, valor);
            else if (valor > nodo.Valor)
                nodo.Der = Insertar(nodo.Der, valor);

            return nodo;
        }

        public void Dibujar(Nodo nodo, int nivel = 0)
        {
            if (nodo == null) return;

            Dibujar(nodo.Der, nivel + 1);
            Console.WriteLine(new string(' ', nivel * 5) + nodo.Valor);
            Dibujar(nodo.Izq, nivel + 1);
        }
    }

    class AVL
    {
        int Altura(Nodo n) => n == null ? 0 : n.Altura;

        int Balance(Nodo n) => n == null ? 0 : Altura(n.Izq) - Altura(n.Der);

        Nodo RotarDerecha(Nodo y)
        {
            Nodo x = y.Izq;
            Nodo T2 = x.Der;

            x.Der = y;
            y.Izq = T2;

            y.Altura = Math.Max(Altura(y.Izq), Altura(y.Der)) + 1;
            x.Altura = Math.Max(Altura(x.Izq), Altura(x.Der)) + 1;

            return x;
        }

        Nodo RotarIzquierda(Nodo x)
        {
            Nodo y = x.Der;
            Nodo T2 = y.Izq;

            y.Izq = x;
            x.Der = T2;

            x.Altura = Math.Max(Altura(x.Izq), Altura(x.Der)) + 1;
            y.Altura = Math.Max(Altura(y.Izq), Altura(y.Der)) + 1;

            return y;
        }

        public Nodo Insertar(Nodo nodo, int valor)
        {
            if (nodo == null)
                return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izq = Insertar(nodo.Izq, valor);
            else if (valor > nodo.Valor)
                nodo.Der = Insertar(nodo.Der, valor);
            else
                return nodo;

            nodo.Altura = 1 + Math.Max(Altura(nodo.Izq), Altura(nodo.Der));

            int balance = Balance(nodo);

            // LL
            if (balance > 1 && valor < nodo.Izq.Valor)
                return RotarDerecha(nodo);

            // RR
            if (balance < -1 && valor > nodo.Der.Valor)
                return RotarIzquierda(nodo);

            // LR
            if (balance > 1 && valor > nodo.Izq.Valor)
            {
                nodo.Izq = RotarIzquierda(nodo.Izq);
                return RotarDerecha(nodo);
            }

            // RL
            if (balance < -1 && valor < nodo.Der.Valor)
            {
                nodo.Der = RotarDerecha(nodo.Der);
                return RotarIzquierda(nodo);
            }

            return nodo;
        }

        public void Dibujar(Nodo nodo, int nivel = 0)
        {
            if (nodo == null) return;

            Dibujar(nodo.Der, nivel + 1);
            Console.WriteLine(new string(' ', nivel * 5) + nodo.Valor);
            Dibujar(nodo.Izq, nivel + 1);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            AVL avl = new AVL();
            ABB abb = new ABB();

            Nodo raizAVL = null;
            Nodo raizABB = null;

            Console.Write("¿Cuántos números aleatorios deseas insertar?: ");
            int n = int.Parse(Console.ReadLine());

            Random rand = new Random();

            Console.WriteLine("\nValores generados:");

            for (int i = 0; i < n; i++)
            {
                int valor = rand.Next(1, 100);
                Console.Write(valor + " ");

                raizABB = abb.Insertar(raizABB, valor); 
                raizAVL = avl.Insertar(raizAVL, valor); 
            }

            Console.WriteLine("\n Árbol NORMAL (sin balancear):\n");
            abb.Dibujar(raizABB);

            Console.WriteLine("\n Árbol AVL (balanceado):\n");
            avl.Dibujar(raizAVL);

            Console.ReadLine();
        }
    }
}
