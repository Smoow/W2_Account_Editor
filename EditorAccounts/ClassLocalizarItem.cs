using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EditorAccounts
{
    public class ClassLocalizarItem2
    {
        public static int LocalizarItens(int index)
        {
            int encontrados = 0;
            int encontradosPorConta = 0;

            try
            {
                External.ContasLocalizadas.Clear();

                DirectoryInfo diretorio = new DirectoryInfo(Environment.CurrentDirectory + @"/account/");
                FileInfo[] files = diretorio.GetFiles(".", SearchOption.AllDirectories);

                foreach (FileInfo fileinfo in files)
                {
                    encontradosPorConta = 0;
                    byte[] data = File.ReadAllBytes(fileinfo.FullName);

                    // Antigo -> Continua bug pela falta de gerenciamento de mem.
                    //Structs.STRUCT_ACCOUNTFILE contas = (Structs.STRUCT_ACCOUNTFILE)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), typeof(Structs.STRUCT_ACCOUNTFILE));
                    //

                    // Novo
                    GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr pointer = pinnedArray.AddrOfPinnedObject();
                    Structs.STRUCT_ACCOUNTFILE contas = (Structs.STRUCT_ACCOUNTFILE)Marshal.PtrToStructure(pointer, typeof(Structs.STRUCT_ACCOUNTFILE));
                    Structs.STRUCT_MOB inv = (Structs.STRUCT_MOB)Marshal.PtrToStructure(pointer, typeof(Structs.STRUCT_MOB));


                    for (int i = 0; i < 128; i++)
                    {
                        if (contas.Cargo[i].sIndex == index)
                        {
                            if (contas.Cargo[i].sEffect[0].cEfeito == 61)
                            {
                                encontradosPorConta += contas.Cargo[i].sEffect[0].cValue;
                                encontrados += (contas.Cargo[i].sEffect[0].cValue);
                            }
                            else
                            {
                                encontradosPorConta += 1;
                                encontrados++;
                            }
                        }

                    }

                    for (int i = 0; i < 64; i++)
                    {
                        if (inv.Carry[i].sIndex == index)
                        {
                            if (inv.Carry[i].sEffect[0].cEfeito == 61)
                            {
                                encontradosPorConta += inv.Carry[i].sEffect[0].cValue;
                                encontrados += (inv.Carry[i].sEffect[0].cValue);
                            }
                            else
                            {
                                encontradosPorConta += 1;
                                encontrados++;
                            }
                        }
                    }

                    for (int i = 0; i < 16; i++)
                    {
                        if (inv.Equip[i].sIndex == index)
                        {
                            encontradosPorConta += 1;
                            encontrados++;
                        }
                    }


                    contas.itensEncontrados = encontradosPorConta;
                    if (encontradosPorConta != 0) External.ContasLocalizadas.Add(contas);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return encontrados;
        }
    }
}
