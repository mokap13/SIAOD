using DBKursovaia.MVVMHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class RichByte : ObservableObject
    {
        public static implicit operator byte(RichByte richByte)
        {
            return richByte.value;
        }
        public static implicit operator RichByte(byte richByte)
        {
            return new RichByte() { IsSelected = false, Value = richByte, IsSuspected = false };
        }
        
        //public static implicit operator byte[](RichByte[] richByte)
        //{
        //    return richByte.Select(r => r.value).ToArray();
        //}
        //public static implicit operator RichByte[](byte[] richByte)
        //{
        //    return new RichByte() { Value = richByte };
        //}
        private byte value;
        public byte Value
        {
            get { return this.value; }
            set { SetProperty(ref this.value, value); }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set { SetProperty(ref this.isSelected, value); }
        }
        private bool isSuspected;
        public bool IsSuspected
        {
            get { return this.isSuspected; }
            set { SetProperty(ref this.isSuspected, value); }
        }
        public override string ToString()
        {
            return this.Value.ToString("X2") + " ";
        }
    }
}
