using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVision
{
    interface IOCRInterface
    {
        string Field { get; set; }
        string firstPosition { get; set; } 
        string lastPosition { get; set; }
       
    }

    class ReadValues : IOCRInterface
    {
        private string _firstPosition;
        private string _lastPosition;
        private string _field;

        public string firstPosition
        {
            get { return this._firstPosition; }
            set { this._firstPosition = value; }
        }

        public string lastPosition
        {
            get { return this._lastPosition; }
            set { this._lastPosition = value; }
        }

        public string Field
        {
            get { return this._field; }
            set { this._field = value; }
        }

       
    }
}
