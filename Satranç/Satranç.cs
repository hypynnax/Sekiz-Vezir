using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Satranç {
    public partial class satrancTahtasi : Form {

        private bool uygunYer = true;
        private int vezirSayisi = 0;
        private int[,] satrancTahtasiDizisi;
        private List<string> koordinatlar;
        private List<string> vezirlerinYerleri;
        private List<PictureBox> kareler = new List<PictureBox>(64);
        private Random random = new Random();

        public satrancTahtasi() {
            InitializeComponent();
            kareler.Add(A1); kareler.Add(A2); kareler.Add(A3); kareler.Add(A4); kareler.Add(A5); kareler.Add(A6); kareler.Add(A7); kareler.Add(A8);
            kareler.Add(B1); kareler.Add(B2); kareler.Add(B3); kareler.Add(B4); kareler.Add(B5); kareler.Add(B6); kareler.Add(B7); kareler.Add(B8);
            kareler.Add(C1); kareler.Add(C2); kareler.Add(C3); kareler.Add(C4); kareler.Add(C5); kareler.Add(C6); kareler.Add(C7); kareler.Add(C8);
            kareler.Add(D1); kareler.Add(D2); kareler.Add(D3); kareler.Add(D4); kareler.Add(D5); kareler.Add(D6); kareler.Add(D7); kareler.Add(D8);
            kareler.Add(E1); kareler.Add(E2); kareler.Add(E3); kareler.Add(E4); kareler.Add(E5); kareler.Add(E6); kareler.Add(E7); kareler.Add(E8);
            kareler.Add(F1); kareler.Add(F2); kareler.Add(F3); kareler.Add(F4); kareler.Add(F5); kareler.Add(F6); kareler.Add(F7); kareler.Add(F8);
            kareler.Add(G1); kareler.Add(G2); kareler.Add(G3); kareler.Add(G4); kareler.Add(G5); kareler.Add(G6); kareler.Add(G7); kareler.Add(G8);
            kareler.Add(H1); kareler.Add(H2); kareler.Add(H3); kareler.Add(H4); kareler.Add(H5); kareler.Add(H6); kareler.Add(H7); kareler.Add(H8);
            vezirBul();
        }

        private void cikisButonu_Click(object sender, System.EventArgs e) {
            Close();
        }

        private void yenilemeButonu_Click(object sender, System.EventArgs e) {
            vezirBul();
        }

        private void sagCaprazKontrol(int[,] tahta, int x, int y) {
            int minNumber = Math.Min(7 - x, y);
            x += minNumber;
            y -= minNumber;
            while (x >= 0 & y <= 7) {
                if (tahta[y, x] == 1) {
                    uygunYer = false;
                    return;
                }
                x -= 1;
                y += 1;
            }
        }

        private void solCaprazKontrol(int[,] tahta, int x, int y) {
            int minNumber = Math.Min(x, y);
            x -= minNumber;
            y -= minNumber;
            while (x <= 7 & y <= 7) {
                if (tahta[y, x] == 1) {
                    uygunYer = false;
                    return;
                }
                x += 1;
                y += 1;
            }
        }

        private void ustAltKontrol(int[,] tahta, int x) {
            for (int i = 0; i < 8; i++) {
                if (tahta[i, x] == 1) {
                    uygunYer = false;
                    return;
                }
            }
        }

        private void sagSolKontrol(int[,] tahta, int y) {
            for (int i = 0; i < 8; i++) {
                if (tahta[y, i] == 1) {
                    uygunYer = false;
                    return;
                }
            }
        }

        private void sagCaprazDoldur(int[,] tahta, int x, int y, int a, int b) {
            int minNumber = Math.Min(7 - x, y);
            x += minNumber;
            y -= minNumber;
            while (x >= 0 & y <= 7) {
                if (tahta[y, x] == 2) {
                    if (x != a & y != b) {
                        koordinatlar.Remove(y + "" + x);
                    }
                    tahta[y, x] = 0;
                }
                x -= 1;
                y += 1;
            }
        }

        private void solCaprazDoldur(int[,] tahta, int x, int y, int a, int b) {
            int minNumber = Math.Min(x, y);
            x -= minNumber;
            y -= minNumber;
            while (x <= 7 & y <= 7) {
                if (tahta[y, x] == 2) {
                    if (x != a & y != b) {
                        koordinatlar.Remove(y + "" + x);
                    }
                    tahta[y, x] = 0;
                }
                x += 1;
                y += 1;
            }
        }

        private void ustAltDoldur(int[,] tahta, int x, int y) {
            for (int i = 0; i < 8; i++) {
                if (tahta[i, x] == 2) {
                    if (i != y) {
                        koordinatlar.Remove(i + "" + x);
                    }
                    tahta[i, x] = 0;
                }
            }
        }

        private void sagSolDoldur(int[,] tahta, int x, int y) {
            for (int i = 0; i < 8; i++) {
                if (tahta[y, i] == 2) {
                    if (i != x) {
                        koordinatlar.Remove(y + "" + i);
                    }
                    tahta[y, i] = 0;
                }
            }
        }

        private void vezirBul() {
            while (true) {
                if (vezirSayisi >= 8) {
                    tahtadaGoster();
                    break;
                }else {
                    vezirSayisi = 0;
                    satrancTahtasiDizisi = new int[8,8];
                    koordinatlar = new List<string>(64);
                    vezirlerinYerleri = new List<string>();
                    int sira = 0;
                    for (int i = 0; i < 8; i++) {
                        for (int j = 0; j < 8; j++) {
                            satrancTahtasiDizisi[i, j] = 2;
                            koordinatlar.Add(i + "" + j);
                            sira++;
                        }
                    }
                }

                for (int i = 0; i < 8; i++) {
                    if (koordinatlar.Count != 0) {
                        string koordinatNoktalari = koordinatlar[random.Next(0, koordinatlar.Count)];
                        int y = Convert.ToInt32(koordinatNoktalari.Substring(0, 1));
                        int x = Convert.ToInt32(koordinatNoktalari.Substring(1, 1));
                        sagCaprazKontrol(satrancTahtasiDizisi, x, y);
                        solCaprazKontrol(satrancTahtasiDizisi, x, y);
                        ustAltKontrol(satrancTahtasiDizisi, x);
                        sagSolKontrol(satrancTahtasiDizisi , y);
                        if (uygunYer) {
                            satrancTahtasiDizisi[y, x] = 1;
                            vezirSayisi += 1;
                            sagCaprazDoldur(satrancTahtasiDizisi, x, y, x, y);
                            solCaprazDoldur(satrancTahtasiDizisi, x, y, x, y);
                            ustAltDoldur(satrancTahtasiDizisi, x, y);
                            sagSolDoldur(satrancTahtasiDizisi, x, y);
                        }else {
                            uygunYer = true;
                        }
                        koordinatlar.Remove(y + "" + x);
                        vezirlerinYerleri.Add(y + "" + x);
                    }
                }
            }
        }

        private void tahtadaGoster() {
            int konum = 0;
            foreach (PictureBox kare in kareler) {
                kare.Image = null;
            }
            foreach (string vezir in vezirlerinYerleri) {
                konum = Convert.ToInt32(vezir.Substring(0, 1)) * 8 + Convert.ToInt32(vezir.Substring(1, 1));
                kareler[konum].Image = Properties.Resources.vezir;
            }
            vezirSayisi = 0;
        }
    }
}
