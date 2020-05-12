using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Windows.Forms;

namespace STV_CADTOOLS
{
    public class Commands
    {
        [CommandMethod("TEST")]
        public void Test()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
            using (var tr = db.TransactionManager.StartTransaction())
            {
                MessageBox.Show("Debug 3");
                tr.Commit();
            }
        }
        [CommandMethod("RenameLayers")]
        public static void RenameLayer()
        {
            // Get the current document and database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Returns the layer table for the current database
                LayerTable acLyrTbl;
                acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId,
               OpenMode.ForWrite) as LayerTable;
                // Clone layer 0 (copy it and its properties) as a new layer
                LayerTableRecord acLyrTblRec;

                IDictionary<string, string> dict = null;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;
                    List<string> fileContent = new List<string>();
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //read file
                        dict = readwriteapp.ReadCSV.Read(openFileDialog.FileName);
                    }
                }
                foreach(string old in dict.Keys)
                {
                    try 
                    { 
                        acLyrTblRec = acTrans.GetObject(acLyrTbl[old], OpenMode.ForWrite) as LayerTableRecord;
                        // Change the name of the cloned layer
                        acLyrTblRec.UpgradeOpen();
                        acLyrTblRec.Name = dict[old];
                    }
                    catch
                    {
                        
                    }
                }


                acTrans.Commit();
            }
        }
        [CommandMethod("RenameLayer")]
        public static void RenameLayers()
        {
            // Get the current document and database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Returns the layer table for the current database
                LayerTable acLyrTbl;
                acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId,
               OpenMode.ForWrite) as LayerTable;
                // Clone layer 0 (copy it and its properties) as a new layer
                LayerTableRecord acLyrTblRec;
                acLyrTblRec = acTrans.GetObject(acLyrTbl["0"],
               OpenMode.ForRead).Clone() as
               LayerTableRecord;
                // Change the name of the cloned layer
                acLyrTblRec.Name = "MyLayer";
                // Add the cloned layer to the Layer table and transaction
                acLyrTbl.Add(acLyrTblRec);
                acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                // Save changes and dispose of the transaction
                acTrans.Commit();
            }
        }
    }




}
