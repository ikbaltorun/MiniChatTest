# MiniChatTest - E-Ticaret & Yapay Zeka Asistanı 🛒🤖
Bu proje, ASP.NET Core API, Entity Framework Core (SQLite), Ollama (Llama 3.1) entegrasyonu ve Docker konteyner mimarisi kullanılarak geliştirilmiş akıllı bir e-ticaret ve sohbet platformudur.

## 📂 Dosya ve Klasör Yapısı
Projenin yapı taşları ve dosyaların yerleri şu şekildedir:

Program.cs (Ana Dizin): Uygulamanın ayağa kalktığı, servislerin ve yönlendirmelerin ayarlandığı ana dosya.

Controllers/ChatController.cs: API isteklerinin ve yapay zeka iletişiminin yönetildiği backend sınıfı.

Models/AppDbContext.cs: Entity Framework Core veritabanı bağlantı şeması.

ecommerce.db (Ana Dizin): Ürünlerin ve güncel stokların saklandığı SQLite veritabanı dosyası (GitHub üzerinden versiyonlanmaktadır).

Dockerfile (Ana Dizin): .NET uygulamasının izole bir çalışma ortamında paketlenmesini sağlayan imaj dosyası.

docker-compose.yml (Ana Dizin): Web servisi ile Ollama yapay zeka motorunu ortak bir sanal ağda buluşturan orkestrasyon dosyası.

wwwroot/index.html: Kullanıcının gördüğü ön yüz (frontend) arayüz dosyası.

## 🚀 Özellikler
SQLite Veritabanı: Ürünlerin ve stokların dinamik olarak yönetildiği altyapı (ecommerce.db).

Yapay Zeka Destekli Asistan: Llama 3.1 modeli ile entegre, teknoloji danışmanlığı yapabilen akıllı chat sistemi.

Dinamik Ürün Kartları: Fiyat, stok durumu, kategori ve puan bilgilerini gösteren modern UI tasarımı.

Akıllı Filtreleme: En ucuz, en pahalı ve kategori bazlı nokta atışı ürün arama mekanizmaları.

Docker & DevOps Altyapısı: Çevre bağımlılıklarını sıfıra indiren, tek komutla ayağa kalkan konteyner mimarisi.

Veritabanı Senkronizasyonu: ecommerce.db dosyasının GitHub üzerinde güncel tutulması sayesinde, eklenen yeni ürünlerin her ortamda ve her indirmede eksiksiz olarak gelmesi.

## 💻 Projeyi Çalıştırma Rehberi
Projeyi test etmek veya incelemek için iki farklı yöntem kullanabilirsiniz:

### Yöntem 1: Docker ile Çalıştırma (Önerilen & Bağımsız)
Herhangi bir işletim sisteminde (Windows, macOS, Linux) ek SDK kurulumlarına ihtiyaç duymadan çalıştırmak için:

Projeyi GitHub'dan indirin ve normal bir klasöre çıkartın.

Ana klasörün içinde terminali (PowerShell, CMD veya Terminal) açın.

Şu komutu çalıştırın:
`` docker-compose up --build -d ``

Tarayıcınızı açın ve http://localhost:8080 adresine giderek sistemi kullanmaya başlayın!

### Yöntem 2: Visual Studio ile Yerel Çalıştırma
Projeyi İndirin: GitHub sayfasındaki yeşil Code butonuna tıklayın ve Download ZIP seçeneği ile projeyi bilgisayarınıza indirin.

Klasöre Çıkartın: İndirdiğiniz ZIP dosyasını sağ tıklayıp normal bir klasöre çıkartın (ZIP içinden doğrudan açmayın).

Visual Studio ile Açın: Çıkardığınız klasörün içine girip MiniChatTest.sln dosyasına çift tıklayarak projeyi Visual Studio ile açın.

Paketlerin Yüklenmesini Bekleyin: Visual Studio açıldığında altta “Restoring NuGet packages...” yazısı çıkacaktır; eksik paketlerin otomatik yüklenmesi için birkaç saniye bekleyin.

Çalıştırın: Visual Studio üst menüsündeki yeşil https çalıştır butonuna basın veya klavyeden F5 tuşuna basın. Tarayıcı otomatik olarak açılacak ve güncel veritabanı ile e-ticaret arayüzü karşınıza gelecektir!
