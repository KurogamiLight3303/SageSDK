using Net4Sage.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.GLUtils
{
    public class JournalAnnotation
    {
        private string _currID;
        private decimal _exchRate;
        private List<JournalAnnotationReference> _references;
        public JournalAnnotation(JournalOffsetPostType type = JournalOffsetPostType.Debit)
        {
            _references = new List<JournalAnnotationReference>();
            _exchRate = 1;
            Type = type;
        }
        public string Journal { get; set; }
        public int JournalNumber { get; set; }
        public string Currency { get => _currID; set
            {
                if(_currID != value)
                {
                    _currID = value;
                    UpdateCurrency();
                }
            }
        }
        public decimal ExchangeRate { get => _exchRate; set
            {
                if(value != _exchRate)
                {
                    _exchRate = value;
                    UpdateCurrency();
                }
            }
        }
        public bool Financial { get; set; }
        public DateTime Date { get; set; }
        public int NextEntryNo { get => _references.Any() ? _references.Max(p => p.No) + 1 : 1; }
        public JournalOffsetPostType Type { get; private set; }
        internal void UpdateCurrency()
        {
            foreach (JournalAnnotationReference i in _references)
            {
                i.ExchRate = ExchangeRate;
                i.CurrID = Currency;
            }
        }

        public void Add(JournalAnnotationReference reference)
        {
            if (!_references.Contains(reference))
            {
                reference.CurrID = Currency;
                reference.ExchRate = ExchangeRate;
                reference.No = NextEntryNo;
                _references.Add(reference);
            }
        }
        public void AddRange(IEnumerable<JournalAnnotationReference> references)
        {
            foreach (JournalAnnotationReference i in references)
                Add(i);
        }
        public void Remove(JournalAnnotationReference reference)
        {
            _references.Remove(reference);
        }

        public IEnumerable<JournalAnnotationReference> GetAnnotationReferences()
        {
            foreach (JournalAnnotationReference i in _references)
                yield return i;
        }

        public bool IsBalance
        {
            get => Desbalance == 0;
        }

        public decimal Desbalance { get => Math.Abs(_references.Sum(p => p.PostAmtHC)); }

        public int Count { get => _references.Count; }
    }
}
