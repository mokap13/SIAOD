using Crypto1.Models;
using Crypto1.Models.Enigma;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace Crypto1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Enigma enigma;
        public ObservableCollection<char> Alphabet { get; set; }

        public char Rotor1Position
        {
            get { return this.enigma.GetPosition(0); }
            set { this.enigma.SetPositionRotor(0, value); }
        }

        public char Rotor2Position
        {
            get { return this.enigma.GetPosition(1); }
            set { this.enigma.SetPositionRotor(1, value); }
        }

        public char Rotor3Position
        {
            get { return this.enigma.GetPosition(2); }
            set { this.enigma.SetPositionRotor(2, value); }
        }

        private string inputText = string.Empty;
        public string InputText
        {
            get { return inputText; }
            set { base.Set(ref this.inputText, value); }
        }

        private string outText = string.Empty;
        public string OutText
        {
            get { return outText; }
            set { base.Set(ref this.outText, value); }
        }

        private readonly RelayCommand cryptCommand = null;
        public ICommand CryptCommand
        {
            get => this.cryptCommand ?? new RelayCommand(() =>
            {
                if (this.InputText.ToUpper().All(c => this.Alphabet.Contains(c)))
                {
                    this.enigma.ResetPositions();
                    this.OutText = this.enigma.CryptString(this.InputText.ToUpper());
                    base.RaisePropertyChanged("Rotor1Position");
                    base.RaisePropertyChanged("Rotor2Position");
                    base.RaisePropertyChanged("Rotor3Position");
                }
            });
        }

        private string key = string.Empty;
        public string Key
        {
            get { return key; }
            set { base.Set(ref key, value); }
        }

        private string inputTextGronsfeld = string.Empty;
        public string InputTextGronsfeld
        {
            get { return inputTextGronsfeld; }
            set { base.Set(ref this.inputTextGronsfeld, value); }
        }

        private string outTextGronsfeld = string.Empty;
        public string OutTextGronsfeld
        {
            get { return outTextGronsfeld; }
            set { base.Set(ref this.outTextGronsfeld, value); }
        }

        private readonly RelayCommand cryptCommandGronsfeld = null;
        public ICommand CryptCommandGronsfeld
        {
            get => this.cryptCommandGronsfeld ?? new RelayCommand(() =>
            {
                if (this.Key.All(c => char.IsDigit(c))
                && this.InputTextGronsfeld.ToLower().All(c => Gronsfeld.Alphabet.Contains(c)))
                {
                    Gronsfeld gronsfeld = new Gronsfeld(this.Key.Select(c => int.Parse(c.ToString())).ToArray());
                    this.OutTextGronsfeld = gronsfeld.CryptText(this.InputTextGronsfeld.ToLower());
                }
            });
        }

        private readonly RelayCommand deCryptCommandGronsfeld = null;
        public ICommand DeCryptCommandGronsfeld
        {
            get => this.deCryptCommandGronsfeld ?? new RelayCommand(() =>
            {
                if (this.Key.All(c => char.IsDigit(c))
                && this.InputTextGronsfeld.ToLower().All(c => Gronsfeld.Alphabet.Contains(c)))
                {
                    Gronsfeld gronsfeld = new Gronsfeld(this.Key.Select(c => int.Parse(c.ToString())).ToArray());
                    this.OutTextGronsfeld = gronsfeld.DecryptText(this.InputTextGronsfeld.ToLower());
                }
            });
        }

        public MainViewModel()
        {
            Alphabet = new ObservableCollection<char>();
            enigma = new Enigma();
            Rotor.Alphabet.ToList().ForEach(c => this.Alphabet.Add(c.Key));
        }
    }
}