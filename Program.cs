using Newtonsoft.Json;

namespace Erercizio_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = "codici_comuni.json";
            string jsonContent = File.ReadAllText(jsonFilePath);

            List<Comune> comuni = JsonConvert.DeserializeObject<List<Comune>>(jsonContent);

            bool riscriviDati;

            do
            {
                riscriviDati = false;

                bool codiceConfermato = true;
                do
                {
                    string nome;
                    do
                    {
                        Console.WriteLine("***** INSERISCI DATI PERSONALI *****");
                        Console.Write("Nome: ");
                        nome = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(nome) || nome.Any(char.IsDigit) || nome.Contains(' '))
                        {
                            Console.WriteLine("Inserisci un nome valido, lungo almeno un carattere e non deve contenere numeri ");
                        }
                    } while (string.IsNullOrEmpty(nome) || nome.Any(char.IsDigit) || nome.Contains(' '));

                    string cognome;
                    do
                    {
                        Console.Write("Cognome: ");
                        cognome = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(cognome) || cognome.Any(char.IsDigit))
                        {
                            Console.WriteLine("Inserisci un cognome valido, lungo almeno un carattere e non deve contenere numeri");
                        }
                    } while (string.IsNullOrEmpty(cognome) || cognome.Any(char.IsDigit));

                    DateTime dataNascita;
                    string inputData;
                    do
                    {
                        Console.Write("Data di Nascita (gg/mm/aaaa): ");
                        inputData = Console.ReadLine();

                        if (!DateTime.TryParse(inputData, out dataNascita))
                        {
                            Console.WriteLine("Inserisci una data di nascita valida nel formato gg/mm/aaaa.");
                        }
                    } while (dataNascita == DateTime.MinValue);

                    string sesso;
                    do
                    {
                        Console.Write("Sesso (M/F): ");
                        sesso = Console.ReadLine();

                        if (!string.Equals(sesso, "M", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(sesso, "F", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Inserisci 'M' per Maschio o 'F' per Femmina.");
                        }
                    } while (!string.Equals(sesso, "M", StringComparison.OrdinalIgnoreCase) && !string.Equals(sesso, "F", StringComparison.OrdinalIgnoreCase));

                    string comuneResidenza;
                    bool comuneValido;
                    do
                    {
                        Console.Write("Comune di Residenza: ");
                        comuneResidenza = Console.ReadLine().Trim();

                        comuneResidenza = comuneResidenza.ToLower();

                        Comune comune = comuni.FirstOrDefault(c => c.DESCRIZIONE_COMUNE.ToLower() == comuneResidenza);

                        string codiceBelfiore = string.Empty;
                        if (comune != null)
                        {
                            codiceBelfiore = comune.CODICE_BELFIORE;
                        }

                        comuneValido = !string.IsNullOrEmpty(codiceBelfiore);

                        if (!comuneValido)
                        {
                            Console.WriteLine("Il comune inserito non è valido. Reinserisci un comune valido.");
                        }
                    } while (!comuneValido);

                    string codiceFiscale = GeneraCodiceFiscale(nome, cognome, dataNascita, sesso, comuneResidenza, comuni);

                    Console.WriteLine($"Il tuo codice fiscale è {codiceFiscale}, confermi?: [S]i/ [N]o ");
                    string confermaCodice = Console.ReadLine();

                    codiceConfermato = confermaCodice.Equals("S", StringComparison.OrdinalIgnoreCase);

                    if (codiceConfermato)
                    {
                        double redditoAnnuale;
                        bool inputValido = false;

                        do
                        {
                            Console.Write("Reddito Annuale: ");
                            if (double.TryParse(Console.ReadLine(), out redditoAnnuale) && redditoAnnuale >= 0)
                            {
                                inputValido = true;
                            }
                            else
                            {
                                Console.WriteLine("Inserisci un numero intero positivo.");
                            }
                        } while (!inputValido);


                        Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, sesso, comuneResidenza, codiceFiscale, redditoAnnuale);

                        double impostaDaVersare = contribuente.Imposta();

                        Console.WriteLine("***** CALCOLO DELL'IMPOSTA DA VERSARE: *****");
                        Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome}");
                        Console.WriteLine($"nato il {contribuente.DataNascita:dd/MM/yyyy}");
                        Console.WriteLine($"residente a {contribuente.ComuneResidenza}");
                        Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
                        Console.WriteLine($"Reddito dichiarato: Euro {contribuente.RedditoAnnuale}");
                        Console.WriteLine($"IMPOSTA DOVUTA: Euro {impostaDaVersare}");
                        Console.WriteLine("Cosa desideri fare ora?: ");
                        Console.WriteLine("1. Calcola nuova imposta");
                        Console.WriteLine("2. Esci");
                        Console.Write("Scelta: ");

                        string scelta = Console.ReadLine();

                        switch (scelta)
                        {
                            case "1":
                                riscriviDati = false;
                                break;
                            case "2":
                                return;
                        }
                    }
                    else
                    {
                        riscriviDati = true;
                        Console.WriteLine(" ***** Inserisci nuovamente i tuoi dati corretti *****");
                    }
                } while (codiceConfermato && !riscriviDati);

            } while (riscriviDati);
        }

        class Contribuente
        {
            public string Nome { get; set; }
            public string Cognome { get; set; }
            public DateTime DataNascita { get; set; }
            public string CodiceFiscale { get; set; }
            public string Sesso { get; set; }
            public string ComuneResidenza { get; set; }
            public double RedditoAnnuale { get; set; }

            public Contribuente(string nome, string cognome, DateTime dataNascita, string sesso, string comuneResidenza, string codiceFiscale, double redditoAnnuale)
            {
                Nome = nome;
                Cognome = cognome;
                DataNascita = dataNascita;
                CodiceFiscale = codiceFiscale;
                Sesso = sesso;
                ComuneResidenza = comuneResidenza;
                RedditoAnnuale = redditoAnnuale;
            }

            public double Imposta()
            {
                double imposta = 0;

                imposta = RedditoAnnuale switch
                {
                    double reddito when reddito <= 15000 => reddito * 0.23,
                    double reddito when reddito <= 28000 => 3450 + (reddito - 15000) * 0.27,
                    double reddito when reddito <= 55000 => 6960 + (reddito - 28000) * 0.38,
                    double reddito when reddito <= 75000 => 17220 + (reddito - 55000) * 0.41,
                    _ => 25420 + (RedditoAnnuale - 75000) * 0.43,
                };
                return imposta;
            }
        }

        public class Comune
        {
            public string DESCRIZIONE_COMUNE { get; set; }
            public string CODICE_BELFIORE { get; set; }
        }

        static string GeneraCodiceFiscale(string nome, string cognome, DateTime dataNascita, string sesso, string comuneResidenza, List<Comune> comuni)
        {
            string consonantiNome = string.Concat(nome.Where(char.IsLetter).Where(c => !"AEIOU".Contains(char.ToUpper(c))));
            string consonantiCognome = string.Concat(cognome.Where(char.IsLetter).Where(c => !"AEIOU".Contains(char.ToUpper(c))));

            string nomeCodice = (consonantiNome.Length > 3 ? consonantiNome[0].ToString() + consonantiNome[2].ToString() + consonantiNome[3].ToString() : consonantiNome.PadRight(3, 'X')).Substring(0, 3);
            string cognomeCodice = (consonantiCognome.Length > 3 ? consonantiCognome[0].ToString() + consonantiCognome[2].ToString() + consonantiCognome[3].ToString() : consonantiCognome.PadRight(3, 'X')).Substring(0, 3);

            string anno = dataNascita.Year.ToString().Substring(2);
            string mese = "ABCDEHLMPRST".Substring(dataNascita.Month - 1, 1);
            string giorno = (dataNascita.Day + (sesso.ToUpper() == "F" ? 40 : 0)).ToString().PadLeft(2, '0');

            string comuneCodice = string.Empty;

            Comune comune = comuni.FirstOrDefault(c => c.DESCRIZIONE_COMUNE.ToLower() == comuneResidenza.ToLower());
            if (comune != null)
            {
                comuneCodice = comune.CODICE_BELFIORE;
            }

            string codiceFiscaleSenzaUltimaCifra = cognomeCodice + nomeCodice + anno + mese + giorno + comuneCodice;

            char cifraFinale = CalcolaCifraDiControllo(codiceFiscaleSenzaUltimaCifra);

            string codiceFiscale = codiceFiscaleSenzaUltimaCifra + cifraFinale;

            return codiceFiscale.ToUpper();
        }

        static char CalcolaCifraDiControllo(string codiceFiscaleSenzaUltimaCifra)
        {
            int somma = 0;

            Dictionary<char, int> valoriDispari = new Dictionary<char, int>
{
    {'0', 1}, {'1', 0}, {'2', 5}, {'3', 7}, {'4', 9}, {'5', 13}, {'6', 15}, {'7', 17}, {'8', 19}, {'9', 21},
    {'A', 1}, {'B', 0}, {'C', 5}, {'D', 7}, {'E', 9}, {'F', 13}, {'G', 15}, {'H', 17}, {'I', 19}, {'J', 21},
    {'K', 2}, {'L', 4}, {'M', 18}, {'N', 20}, {'O', 11}, {'P', 3}, {'Q', 6}, {'R', 8}, {'S', 12}, {'T', 14},
    {'U', 16}, {'V', 10}, {'W', 22}, {'X', 25}, {'Y', 24}, {'Z', 23}
};

            Dictionary<char, int> valoriPari = new Dictionary<char, int>
{
    {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
    {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6}, {'H', 7}, {'I', 8}, {'J', 9},
    {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13}, {'O', 14}, {'P', 15}, {'Q', 16}, {'R', 17}, {'S', 18}, {'T', 19},
    {'U', 20}, {'V', 21}, {'W', 22}, {'X', 23}, {'Y', 24}, {'Z', 25}
};

            Dictionary<int, char> restoToLettera = new Dictionary<int, char>
{
    {0, 'A'}, {1, 'B'}, {2, 'C'}, {3, 'D'}, {4, 'E'}, {5, 'F'}, {6, 'G'}, {7, 'H'}, {8, 'I'}, {9, 'J'},
    {10, 'K'}, {11, 'L'}, {12, 'M'}, {13, 'N'}, {14, 'O'}, {15, 'P'}, {16, 'Q'}, {17, 'R'}, {18, 'S'}, {19, 'T'},
    {20, 'U'}, {21, 'V'}, {22, 'W'}, {23, 'X'}, {24, 'Y'}, {25, 'Z'}
};

            codiceFiscaleSenzaUltimaCifra = codiceFiscaleSenzaUltimaCifra.ToUpper();

            for (int i = 0; i < codiceFiscaleSenzaUltimaCifra.Length; i++)
            {
                char carattere = codiceFiscaleSenzaUltimaCifra[i];
                int valore = ((i + 1) % 2 == 0) ? valoriPari[carattere] : valoriDispari[carattere];
                somma += valore;
            }

            int resto = somma % 26;
            return restoToLettera[resto];
        }
    }
}