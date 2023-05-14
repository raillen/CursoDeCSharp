// See https://aka.ms/new-console-template for more information

namespace Properties
{
    class Program
    {
        static void Main(String[] args) {
            Produto p = new Produto("T", 500.0f, 5);

            Console.WriteLine(p.ToString());
            Console.WriteLine(p.Preco);
            Console.WriteLine("Nome" + p.Nome);
        }
    }
}


class Produto
{
    private string _nome;
    // propriedades autoimplementadas
    public float Preco { get; private set; }
    private int _quantidade;

    // propriedades
    public string Nome
    {
        get { return _nome; }
        set
        {
            if (value == null || value.Length <= 1)
            {
               throw new ArgumentException("valor errado");
            }
            else
            {
                _nome = value;
            }
        }
    }


    public int Quantidade { get { return _quantidade; } }

    // construtores
    public Produto() { }
    public Produto(string nome, float preco)
    {
        _nome = nome;
        Preco = preco;
    }
    public Produto(string nome, float preco, int quantidade) : this(nome, preco)
    {
        _quantidade = quantidade;
    }

    //métodos
    public override string ToString()
    {
        return $"--- Produto ---\nNome: {_nome}\nPreço:{Preco.ToString("F2")}\nQnt: {_quantidade}";
    }
}