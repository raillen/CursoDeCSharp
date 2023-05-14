using System.Text;
using System.Threading;

Menu.RunProgram();

class BankAccount
{
    public int Id{ get; private set; }
    public float Balance { get; private set; }
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (value.Length > 1) _name = value;
        }
    }

    public BankAccount() { }

    public BankAccount(string name, string accountNumber)
    {
        Balance = 0;
        Name = name;
        if (!int.TryParse(accountNumber, out int result))
        {
            throw new Exception("Número da conta errado!");
        }
        else
        {
            Id = result;
        }
    }
    public BankAccount(string name, string accountNumber, float deposit) :this(name, accountNumber)
    {
        if(deposit > 0) {
            Balance += deposit;
        }
        else
        {
            throw new Exception("Não é possível adicionar valor negativo!");
        }
    }

    public void Deposit(float balance)
    {
        Balance += balance;
    }

    public void WithDraw(float balance)
    {
        if(Balance >= balance)
        {
            Balance -= balance;
        }
        else
        {
            throw new Exception("Insuficient Value!");
        }
    }
    public override string ToString()
    {
        return $"---- Dados da Conta ----\nNome: {Name}\nSaldo: {Balance}";
    }
}

class Database
{
    private static List<BankAccount> bankAcccounts = new List<BankAccount>();
    private static StringBuilder _sb = new StringBuilder();
    public static void AddAccount(BankAccount _bankAccount)
    {
        bankAcccounts.Add(_bankAccount);
    }

    public static string ConsultAccount(int accountId)
    {
        BankAccount searchAccount = bankAcccounts.Find(acc => acc.Id == accountId);
        if(searchAccount == null)
        {
            throw new Exception("No located account");
        }
        else
        {
            return searchAccount.ToString();
        }
    }
    public static string ListAccounts()
    {
        _sb.Clear();

        foreach (BankAccount _bankAcc in bankAcccounts)
        {
            _sb.Append($"Account: {_bankAcc.Id} || Name: {_bankAcc.Name} || Balance: {_bankAcc.Balance}\n");
        }

        return _sb.ToString();
    }
}

class Menu
{
    public static string ShowMenu()
    {
        return "Pressione...\n(1) Cadastro de conta\n2) Consultar Conta\n3) Listar todas as contas\n0) Sair\nOpção: "; 
    }

    public static string SelectMenu(string opt)
    {
        return opt;
    }

    public static void RunProgram()
    {
        Console.Clear();
        Console.Write(Menu.ShowMenu());
        string opcao = Menu.SelectMenu(Console.ReadLine());
        string backToMenu = " ";

        if (opcao != null)
        {
            switch (opcao[0])
            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("---- Digite os dados da conta");

                    Console.Write("Nome do cliente: ");
                    string clientName = Console.ReadLine();

                    Console.Write("Número da Conta: ");
                    string accNumber = Console.ReadLine();

                    BankAccount acc = new BankAccount(clientName, accNumber);

                    Console.Write("Irá ter depósito inicial? (s) SIM (n) NÃO: ");
                    string choice = Console.ReadLine();


                    if (choice[0] == 's')
                    {
                        Console.Write("Saldo Inicial: ");
                        float deposit = float.Parse(Console.ReadLine());
                        acc.Deposit(deposit);
                    }

                    Database.AddAccount(acc);

                    Console.Write("Conta Cadastrada com sucesso!");
                    Thread.Sleep(1000);

                    Menu.RunProgram();
                    break;
                case '2':
                    Console.Clear();
                    Console.Write("Digite o número da conta: ");
                    int conta = int.Parse(Console.ReadLine());

                    Console.WriteLine(Database.ConsultAccount(conta));

                    while (backToMenu[0] != '0') {
                        Console.Write("Digite \"0\" to back to the main menu: ");
                        backToMenu = Console.ReadLine();
                    }

                    Menu.RunProgram();
                    break;
                case '3':
                    Console.Clear();
                    Console.WriteLine(Database.ListAccounts());

                    while (backToMenu[0] != '0')
                    {
                        Console.Write("Digite \"0\" to back to the main menu: ");
                        backToMenu = Console.ReadLine();
                    }
                    RunProgram();
                    break;
                case '0':
                    
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong option!!");
                    Thread.Sleep(1000);
                    RunProgram();
                    break;
            }
        }
        else { Console.Write("Wrong option, press \"9\" to back to the main menu."); }

    }
}