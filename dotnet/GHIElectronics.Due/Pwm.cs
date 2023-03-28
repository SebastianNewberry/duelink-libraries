using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHIElectronics.Due {
    public partial class DueController {
        public class PwmController {

            SerialInterface serialPort;

            public PwmController(SerialInterface serialPort) => this.serialPort = serialPort;

            public int Fixed_Frequency { get; } = 50;


            public bool Set(int pin, int dutycyle) {
                if (pin < 0 || pin >= MAX_IO)
                    throw new ArgumentOutOfRangeException("Invalid pin.");

                if (dutycyle < 0 || dutycyle > 1000) {
                    throw new Exception("Dutycle must be in 0..1000");
                }

                var cmd = string.Format("awrite({0},{1})", pin.ToString(), dutycyle.ToString());
     

                this.serialPort.WriteLine(cmd);

                var res = this.serialPort.ReadRespone();

                if (res.success) {
                    return true;
                }

                return false;
            }

        }
    }
}
