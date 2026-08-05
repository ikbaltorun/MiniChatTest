# MiniChatTest - E-Ticaret & Yapay Zeka Asistanı 🛒🤖

Bu proje, ASP.NET Core API, Entity Framework Core (SQLite) ve Ollama (Llama 3.1) entegrasyonu kullanılarak geliştirilmiş akıllı bir e-ticaret ve sohbet platformudur.

---

## 📂 Dosya ve Klasör Yapısı

Projenin yapı taşları ve dosyaların yerleri şu şekildedir:

- **`Program.cs`** *(Ana Dizin)*: Uygulamanın ayağa kalktığı, servislerin ve yönlendirmelerin ayarlandığı ana dosya.
- **`Controllers/ChatController.cs`**: API isteklerinin ve yapay zeka iletişiminin yönetildiği backend sınıfı.
- **`Models/AppDbContext.cs`**: Entity Framework Core veritabanı bağlantı şeması.
- **`ecommerce.db`** *(Ana Dizin)*: Projenin SQLite veritabanı dosyası.
- **`wwwroot/index.html`**: Kullanıcının gördüğü ön yüz (frontend) arayüz dosyası.

---

## 🚀 Özellikler
- **SQLite Veritabanı:** Ürünlerin ve stokların dinamik olarak yönetildiği altyapı (`ecommerce.db`).
- **Yapay Zeka Destekli Asistan:** Llama 3.1 modeli ile entegre, teknoloji danışmanlığı yapabilen akıllı chat sistemi.
- **Dinamik Ürün Kartları:** Fiyat, stok durumu, kategori ve puan bilgilerini gösteren modern UI tasarımı.
- **Akıllı Filtreleme:** En ucuz, en pahalı ve kategori bazlı nokta atışı ürün arama mekanizmaları.

---

## 💻 Projeyi Çalıştırma Rehberi

Projeyi test etmek veya incelemek için şu adımları izleyebilirsiniz:

1. **Projeyi İndirin:** GitHub sayfasındaki yeşil **Code** butonuna tıklayın ve **Download ZIP** seçeneği ile projeyi bilgisayarınıza indirin.
2. **Klasöre Çıkartın:** İndirdiğiniz ZIP dosyasını sağ tıklayıp normal bir klasöre çıkartın *(Asla ZIP içinden doğrudan açmayın)*.
3. **Visual Studio ile Açın:** Çıkardığınız klasörün içine girip **`MiniChatTest.sln`** dosyasına çift tıklayarak projeyi Visual Studio ile açın.
4. **Paketlerin Yüklenmesini Bekleyin:** Visual Studio açıldığında altta *“Restoring NuGet packages...”* yazısı çıkacaktır; eksik paketlerin otomatik yüklenmesi için birkaç saniye bekleyin.
5. **Çalıştırın:** Visual Studio üst menüsündeki yeşil **`https`** çalıştır butonuna basın veya klavyeden **`F5`** tuşuna basın. 
6. Tarayıcı otomatik olarak açılacak ve e-ticaret arayüzü karşınıza gelecektir!
