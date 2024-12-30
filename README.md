# Aplikasi Sederhana Perpustakaan

Aplikasi ini adalah aplikasi berbasis **Windows Forms** yang dikembangkan menggunakan **C# (.NET Framework)** dan menggunakan **SQL Database** untuk menyimpan data secara terstruktur. Aplikasi ini dirancang untuk memenuhi kebutuhan perpustakaan kecil, baik untuk **anggota** maupun **petugas/staff** perpustakaan. Berikut adalah fitur-fitur utama yang ditawarkan:

## FITUR UTAMA APLIKASI

### 📊 **Dashboard Utama**
- Menampilkan total jumlah buku yang tersedia saat ini
- Menampilkan total jumlah anggota yang telah bergabung


### 👤 **Mode Anggota (Member)**
Untuk pengguna yang memilih peran sebagai **anggota**, aplikasi akan menyediakan fitur berikut:
1. **Melihat Koleksi Buku**  
   Anggota dapat melihat seluruh koleksi buku yang tersedia di perpustakaan, termasuk informasi status ketersediaannya
   
2. **Pencarian Buku**  
   - Anggota dapat mencari buku berdasarkan kata kunci
   - Tersedia filter untuk menyaring buku berdasarkan **genre**

3. **Peminjaman Buku**  
   - Anggota dapat meminjam buku yang berstatus **tersedia**.  
   - Jika buku yang dipilih berstatus **tidak tersedia**, peminjaman tidak dapat dilakukan
   - Setelah buku berhasil dipinjam, aplikasi akan otomatis memperbarui status buku menjadi **tidak tersedia** di **database**


### 🔐 **Mode Staff**
Untuk pengguna yang memilih peran sebagai **staff/admin**, terdapat langkah **verifikasi** sebelum mengakses fitur admin:
1. **Login Staff**  
   Admin perlu memasukkan **nama** dan **password** untuk memverifikasi identitasnya. Data admin disimpan di **SQL Database**

Setelah berhasil login, staff dapat mengakses halaman khusus untuk mengelola data perpustakaan, termasuk koleksi buku, data anggota, data staff dan proses peminjaman
2. **CRUD Daftar Member**  
   - Melihat Daftar Member: Admin dapat melihat seluruh daftar member yang terdaftar di perpustakaan
   - Pencarian dan Penyortiran: Admin bisa mencari dan menyortir daftar member berdasarkan kriteria tertentu untuk mempermudah pencarian
   - Menambah Member Baru: Admin dapat menambahkan data member baru. Sistem akan memastikan semua kolom wajib diisi
   - Mengedit Data Member: Admin dapat memperbarui data member yang sudah ada, seperti informasi kontak
   - Menghapus Data Member: Admin dapat menghapus data member yang tidak aktif atau sudah tidak diperlukan lagi
   - Validasi Data: Sistem memastikan bahwa data yang dimasukkan tidak duplikat dan tidak ada kolom yang kosong, menjaga kualitas dan keakuratan data
   
3. **CRUD Koleksi Buku**  
   - Melihat Daftar Buku: Admin dapat melihat seluruh koleksi buku yang ada di perpustakaan
   - Pencarian dan Penyortiran Buku: Admin bisa mencari buku berdasarkan judul, pengarang, atau kategori, serta menyortir buku sesuai dengan kriteria yang diinginkan
   - Menambah Buku Baru: Admin dapat menambahkan buku baru ke koleksi perpustakaan, memastikan informasi yang lengkap dan valid, serta menghindari duplikasi data
   - Mengedit Data Buku: Admin dapat memperbarui data buku yang sudah ada, seperti mengganti judul, pengarang, atau kategori
   - Menghapus Buku: Admin dapat menghapus buku dari koleksi perpustakaan jika buku tersebut tidak relevan atau sudah tidak ada
   - Validasi Data: Setiap data yang dimasukkan akan divalidasi untuk memastikan tidak ada duplikasi atau data yang kosong
   
4. **CRUD Data Staff**  
   - Melihat Daftar Staff: Admin dapat melihat seluruh daftar staf yang bekerja di perpustakaan
   - Pencarian dan Penyortiran Staf: Admin bisa mencari dan menyortir daftar staf berdasarkan nama, jabatan, atau status kerja
   - Menambah Staff Baru: Admin dapat menambahkan staf baru ke sistem, memastikan semua data yang diperlukan diisi dengan lengkap
   - Mengedit Data Staf: Admin dapat memperbarui informasi staf yang ada, seperti jabatan, alamat, atau status kerja
   - Menghapus Data Staf: Admin dapat menghapus staf yang sudah tidak aktif atau tidak lagi bekerja di perpustakaan
   - Validasi Data: Seperti pada fitur lainnya, sistem memastikan bahwa tidak ada data yang duplikat atau kosong, menjaga integritas informasi staf

5. **Proses Peminjaman**  
   Selain member perpustakan, staff juga dapat melakukan proses peminjaman buku. Dipastikan staff hanya dapat meminjam buku yang statusnya tersedia. 
   - Informasi Kumpulan Tabel Peminjaman: Menampilkan informasi mengenai nama member yang meminjam, judul buku yang dipinjam, tanggal peminjaman, dan tanggal pengembalian yang harus dipenuhi
   - Pencarian dan Penyortiran Staff: Admin bisa mencari dan menyortir daftar staff berdasarkan nama
   - Melakukan peminjaman : Admin dapat membantu meminjamkan buku kepada member. Setelah peminjaman dilakukan, status buku otomatis berubah menjadi "Not Available," mencegah anggota lain meminjam buku yang sama.
   - Pengembalian Buku / Penghapusan Proses Peminjaman: Proses pengembalian buku hanya dapat dilakukan oleh staff dimana staff harus memeriksa kondisi baik terlebih dahulu. Setelah itu staff dapat menghapus proses peminjaman, yang akan mengubah status buku      kembali menjadi "Available."
   - Validasi Data: Sistem memastikan tidak ada data duplikat dan kolom yang kosong saat melakukan peminjaman


---
## SISTEM APLIKASI (DATABASE) 💾

### Conceptual Data Model
![471487726_1306902703816272_6596361988404368097_n](https://github.com/user-attachments/assets/c01ce06c-209f-4fd5-aae8-933d4dc6a21e)

### Physical Data Model
![471189462_3108277179314091_2583773187501219093_n](https://github.com/user-attachments/assets/abcf49c1-ad5b-47d3-9a3a-83d5738451a1)

### Class Diagram
![471459860_1795748104571017_5462642717147078027_n](https://github.com/user-attachments/assets/62398df8-441f-49a2-89e4-9c32ad2a00a2)

### SQL Syntax
🔗 https://drive.google.com/file/d/135SGVTR9LhHaYSOEfqkUDphO8doXVTnX/view?usp=sharing 

### Tabel Database
🔗 https://docs.google.com/document/d/1mO8Jw-GVX7UQbMcndoqXhDKuFMXVJ0ow-klPQVS6ugk/edit?usp=sharing

---
## User Scenario Testing
![Untitled spreadsheet - Sheet1-3_page-0001](https://github.com/user-attachments/assets/e3b06589-34e7-4d4b-84e3-8096e84ed9e2)
![Untitled spreadsheet - Sheet1-3_page-0002](https://github.com/user-attachments/assets/4f0ced55-644f-4496-97d2-f447547d1532)
![Untitled spreadsheet - Sheet1-3_page-0003](https://github.com/user-attachments/assets/5880905f-5afd-4e10-89a3-dbadb7282712)
🔗 https://drive.google.com/file/d/1v2lIWziNalbe_FLFbw7O_MhDpShVTRv-/view?usp=share_link

Terima kasih 🚀
