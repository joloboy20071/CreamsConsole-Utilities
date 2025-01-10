using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace CreamsConsole_utils.src
{
    public class eventqueue
    {

        public static ConcurrentQueue<TermialWriterequest> Queuecon;





        public eventqueue() {
            Queuecon = new ConcurrentQueue<TermialWriterequest>(); 
            Task writeTask = Task.Run(() => { MainLoopWriteThread(); });


        }
        

        private static void MainLoopWriteThread() {

            while (true) {
                TermialWriterequest termialWriterequest2;
                TermialWriterequest termialWriterequest1;
                bool termialWriterequest = Queuecon.TryDequeue(out termialWriterequest1);
                if (termialWriterequest)
                {
                    if (termialWriterequest1.message != string.Empty)
                    {
                        conboxFunc.BoxInfoWriteMulti(termialWriterequest1.Box, termialWriterequest1.startPos, termialWriterequest1.message, termialWriterequest1.Style);

                    }
                    else { throw new Exception(); }
                }
                else { Thread.Sleep(10); }
                

            }
        
        
        
        
        }
        public static bool checkenqueue(TermialWriterequest req) {

            int trys = 0; 
            bool valid = true;
            while (valid) 
            {
                Queuecon.Enqueue(req);
                
                TermialWriterequest reqrespond;

                //if (reqrespond == req)
                {
                    
                    valid = false;

                }
                trys += 1;
            }
            return valid;
            
        }




       

       }




        //public void writeTest(BoxInfo box) {




            //TermialWriterequest termialWriterequest2 = new TermialWriterequest(box, "ik ben een termial write request", new UCOORD(4, 6), new WritingStyle());
            //TermialWriterequest termialWriterequest = new TermialWriterequest(box, "ik ben een termial write request", new UCOORD(4, 4), new WritingStyle());
            //Task task = Task.Run(() => {

                
                
            //    checkenqueue(termialWriterequest2);
            //    Thread.Sleep(1000);
               

            //    Task task1 = Task.Run(() =>
            //    {
                    
            //        checkenqueue(termialWriterequest);
                    

            //    });
            //    checkenqueue(new TermialWriterequest(box, "pimpamet ik vlieg door jou flat", new UCOORD(10, 7), new WritingStyle()));

            //});






            //    MainLoopWriteThread();














            //}

       


}
