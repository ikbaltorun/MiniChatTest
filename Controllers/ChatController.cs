using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using MiniChatTest.Models;

namespace MiniChatTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public class UserMessage
        {
            public string? Text { get; set; }
        }

        private static readonly HttpClient client = new HttpClient();

        // VERİTABANI BAĞLANTISI
        private readonly AppDbContext _db;

        public ChatController(AppDbContext db)
        {
            _db = db;
            _db.Database.EnsureCreated();

            // Veritabanı boşsa başlangıç ürünlerini ekler
            if (!_db.Products.Any())
            {
                _db.Products.AddRange(new List<Product>
                {
                    new Product { Name = "Laptop X", Price = 25000, Category = "Laptop", ImageUrl = "/images/laptop.jpg", Stok=10, Puan=8, Description="Yüksek performanslı işlemcisi ve şık tasarımıyla hem yazılım geliştirme hem de günlük işleriniz için ideal dizüstü bilgisayar."},
                    new Product { Name = "Telefon X", Price = 15000, Category = "Telefon", ImageUrl = "/images/telefon.jpg", Stok=12, Puan=7, Description="Yüksek çözünürlüklü kamera kalitesi, akıcı ekran akışı ve gün boyu süren güçlü bataryasıyla modern akıllı telefon deneyimi."},
                    new Product { Name = "Klavye X", Price = 1500, Category = "Klavye", ImageUrl = "/images/klavye.jpg", Stok=9, Puan=5, Description="Mekanik tuş hissiyatı, özelleştirilebilir RGB aydınlatması ve ergonomik yapısıyla konforlu yazım deneyimi sunan klavye."},
                    new Product { Name = "Monitör", Price = 6000, Category = "Monitör", ImageUrl = "/images/monitor.jpg", Stok=18, Puan=9, Description="27 inç geniş ekranı, canlı renk doğruluğu ve yüksek yenileme hızıyla mükemmel görüntü kalitesi."},
                    new Product { Name = "Kulaklık", Price = 1200, Category = "Kulaklık", ImageUrl = "/images/kulaklık.jpg", Stok=11, Puan=4, Description="Kablosuz özgürlük sunan Bluetooth bağlantısı, derin bas performansları ile üstün ses deneyimi." },
                    new Product { Name = "Laptop Y", Price = 22500, Category = "Laptop", ImageUrl = "/images/laptop2.jpg", Stok=10, Puan=6, Description="Yüksek performanslı işlemcisi ve gelişmiş soğutma sistemiyle ağır yazılım projeleriniz ve oyunlar için kesintisiz güç."},
                    new Product { Name = "Telefon Y", Price = 18000, Category = "Telefon", ImageUrl = "/images/telefon2.jpg", Stok=16, Puan=6, Description="Çerçevesiz OLED ekranı ve yapay zeka destekli enerji tasarrufuyla mobil deneyimi zirveye taşıyan model."},
                    new Product { Name = "Klavye Y", Price = 1200, Category = "Klavye", ImageUrl = "/images/klavye2.jpg", Stok=13, Puan=7, Description="Ergonomik bilek desteği ve mekanik tuş hissiyatı ile parmak yorgunluğunu en aza indiren özel tasarım klavye." }
                });
                _db.SaveChanges();
            }
        }

        // ORTAK ÜRÜN KARTI OLUŞTURUCU
        private string UrunKartiOlustur(Product p, string baslik = "")
        {
            string stokRenk = p.Stok < 5 ? "#ef4444" : "#10b981";
            string html = "";

            if (!string.IsNullOrEmpty(baslik))
            {
                html += $"<h3 style='color: #0f172a; margin-bottom: 15px; font-size: 18px;'>{baslik}</h3>";
            }

            html += $@"
            <div style='display: flex; gap: 15px; background: white; padding: 15px; margin-bottom: 15px; border-radius: 12px; box-shadow: 0 4px 12px rgba(0,0,0,0.06); border: 1px solid #e2e8f0; align-items: center;'>
                <img src='{p.ImageUrl}' style='width: 110px; height: 110px; object-fit: cover; border-radius: 8px; border: 1px solid #f1f5f9; flex-shrink: 0;' />
                <div style='flex: 1;'>
                    <div style='display: flex; justify-content: space-between; align-items: flex-start;'>
                        <div style='font-size: 17px; font-weight: bold; color: #0f172a;'>{p.Name} <span style='font-size: 13px; color: #64748b; font-weight: normal; margin-left: 5px; padding: 2px 6px; background: #f1f5f9; border-radius: 4px;'>{p.Category}</span></div>
                        <div style='background: #fffbeb; color: #d97706; padding: 4px 8px; border-radius: 6px; font-size: 13px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.05);'>⭐ {p.Puan}/10</div>
                    </div>
                    
                    <div style='font-size: 13px; color: #64748b; margin: 10px 0; line-height: 1.5; display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden;'>
                        {p.Description}
                    </div>
                    
                    <div style='display: flex; justify-content: space-between; align-items: center; margin-top: 5px;'>
                        <div style='font-size: 19px; font-weight: 900; color: #0284c7;'>{p.Price} ₺</div>
                        <div style='font-size: 13px; font-weight: 800; color: {stokRenk}; background: {stokRenk}15; padding: 4px 10px; border-radius: 6px;'>📦 Stok: {p.Stok}</div>
                    </div>
                </div>
            </div>";
            return html;
        }

        [HttpPost("send")]
        public async Task<IActionResult> GetResponse([FromBody] UserMessage request)
        {
            string message = request.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(new { Reply = "Boş mesaj gönderilemez!" });
            }

            var _products = _db.Products.ToList();

            // 1. ÜRÜNLERİ LİSTELEME
            if (message == "Ürünleri Listeleme")
            {
                var sb = new StringBuilder("<h3 style='color: #0f172a; margin-bottom: 15px; font-size: 18px;'>📦 Sistemdeki Tüm Ürünler</h3>");
                foreach (var p in _products)
                {
                    sb.Append(UrunKartiOlustur(p));
                }
                return Ok(new { Reply = sb.ToString() });
            }

            // 2. EN PAHALI ÜRÜNÜ ARAMA
            if (message.Contains("en pahalı", StringComparison.OrdinalIgnoreCase))
            {
                var matchingProducts = _products.Where(p =>
                    message.Contains(p.Name, StringComparison.OrdinalIgnoreCase) ||
                    message.Contains(p.Category, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Product? targetProduct = matchingProducts.Any()
                    ? matchingProducts.OrderByDescending(x => x.Price).FirstOrDefault()
                    : _products.OrderByDescending(x => x.Price).FirstOrDefault();

                string titleText = matchingProducts.Any() ? "💎 Aradığınız Kriterdeki En Pahalı Ürün" : "💎 Sistemdeki En Pahalı Ürün";

                if (targetProduct != null)
                {
                    return Ok(new { Reply = UrunKartiOlustur(targetProduct, titleText) });
                }
            }

            // 3. EN UCUZ ÜRÜNÜ ARAMA
            if (message.Contains("en ucuz", StringComparison.OrdinalIgnoreCase))
            {
                var matchingProducts = _products.Where(p =>
                    message.Contains(p.Name, StringComparison.OrdinalIgnoreCase) ||
                    message.Contains(p.Category, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Product? targetProduct = matchingProducts.Any()
                    ? matchingProducts.OrderBy(x => x.Price).FirstOrDefault()
                    : _products.OrderBy(x => x.Price).FirstOrDefault();

                string titleText = matchingProducts.Any() ? "🏷️ Aradığınız Kriterdeki En Ucuz Ürün" : "🏷️ Sistemdeki En Ucuz Ürün";

                if (targetProduct != null)
                {
                    return Ok(new { Reply = UrunKartiOlustur(targetProduct, titleText) });
                }
            }

            // 4. EN YÜKSEK PUANLI ÜRÜN ARAMA
            if (message.Contains("en yüksek", StringComparison.OrdinalIgnoreCase))
            {
                var matchingProducts = _products.Where(p =>
                    message.Contains(p.Name, StringComparison.OrdinalIgnoreCase) ||
                    message.Contains(p.Category, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Product? targetProduct = matchingProducts.Any()
                    ? matchingProducts.OrderByDescending(x => x.Puan).FirstOrDefault()
                    : _products.OrderByDescending(x => x.Puan).FirstOrDefault();

                string titleText = matchingProducts.Any() ? "⭐ Aradığınız Kriterdeki En Yüksek Puanlı Ürün" : "⭐ Sistemdeki En Yüksek Puanlı Ürün";

                if (targetProduct != null)
                {
                    return Ok(new { Reply = UrunKartiOlustur(targetProduct, titleText) });
                }
            }

            // 5. EN DÜŞÜK PUANLI ÜRÜN ARAMA
            if (message.Contains("en düşük", StringComparison.OrdinalIgnoreCase))
            {
                var matchingProducts = _products.Where(p =>
                    message.Contains(p.Name, StringComparison.OrdinalIgnoreCase) ||
                    message.Contains(p.Category, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Product? targetProduct = matchingProducts.Any()
                    ? matchingProducts.OrderBy(x => x.Puan).FirstOrDefault()
                    : _products.OrderBy(x => x.Puan).FirstOrDefault();

                string titleText = matchingProducts.Any() ? "📉 Aradığınız Kriterdeki En Düşük Puanlı Ürün" : "📉 Sistemdeki En Düşük Puanlı Ürün";

                if (targetProduct != null)
                {
                    return Ok(new { Reply = UrunKartiOlustur(targetProduct, titleText) });
                }
            }

            // 6. ÜRÜN BİLGİ / DETAY SORGULAMA
            if (message.Contains("bilgi", StringComparison.OrdinalIgnoreCase) ||
                message.Contains("detay", StringComparison.OrdinalIgnoreCase) ||
                message.Contains("özellik", StringComparison.OrdinalIgnoreCase))
            {
                var matchingProducts = _products.Where(p =>
                    message.Contains(p.Name, StringComparison.OrdinalIgnoreCase) ||
                    message.Contains(p.Category, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                if (matchingProducts.Any())
                {
                    var sb = new StringBuilder("<h3 style='color: #0f172a; margin-bottom: 15px; font-size: 18px;'>ℹ️ Eşleşen Ürünler ve Bilgileri</h3>");
                    foreach (var p in matchingProducts)
                    {
                        sb.Append(UrunKartiOlustur(p));
                    }
                    return Ok(new { Reply = sb.ToString() });
                }
            }

            // 7. ÜRÜN ARA BUTONU
            if (message == "Ürün Ara")
            {
                return Ok(new { Reply = "🔍 Lütfen aramak istediğiniz ürünün adını yazınız (Örneğin: 'Laptop', 'Klavye' vb.)." });
            }

            // 8. ADA GÖRE ÜRÜN ARAMA FİLTRESİ
            var foundProducts = _products.Where(p => p.Name.Contains(message, StringComparison.OrdinalIgnoreCase)).ToList();
            if (foundProducts.Any() && message.Length > 1)
            {
                var sb = new StringBuilder("<h3 style='color: #0f172a; margin-bottom: 15px; font-size: 18px;'>🔍 Arama Sonuçları</h3>");
                foreach (var p in foundProducts)
                {
                    sb.Append(UrunKartiOlustur(p));
                }
                return Ok(new { Reply = sb.ToString() });
            }

            // 9. DİĞER MESAJLAR İÇİN OLLAMA (YAPAY ZEKA DESTEKLİ ASİSTAN)
            try
            {
                string ollamaUrl = "http://192.168.5.200:11434/api/chat";

                string productCatalogue = string.Join("\n", _products.Select(p =>
                    $"- Ürün: {p.Name} | Kategori: {p.Category} | Fiyat: {p.Price} TL | Stok: {p.Stok} | Puan: {p.Puan}/10\n  Açıklama: {p.Description}"));

                string systemPrompt =
                    "Sen TechStore'un uzman ve profesyonel teknoloji danışmanısın.\n" +
                    "Kullanıcılara ürün kataloğundaki bilgileri kullanarak yardımcı ol.\n\n" +
                    "KRİTİK KURALLAR:\n" +
                    "1. DİL: Müşteri hangi dilde yazarsa TAMAMEN o dilde yanıt ver. Asla dil karıştırma.\n" +
                    "2. GÖRÜNÜM: Cevaplarını her zaman düzenli HTML formatında ver (<b>, <br>, paragraflar kullan).\n" +
                    "3. EMOJİ: Yanıtlarını teknolojik emojilerle zenginleştir.\n" +
                    "4. OYUNCU TAVSİYESİ: Oyun performansı sorulursa; CS2, Sims 4 veya Stardew Valley gibi senaryolar üzerinden bilgi ver.\n\n" +
                    $"Güncel Ürün Kataloğu:\n{productCatalogue}";

                var payload = new
                {
                    model = "llama3.1:8b",
                    messages = new[]
                    {
                        new { role = "system", content = systemPrompt },
                        new { role = "user", content = message }
                    },
                    stream = false
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ollamaUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonResponse);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("message", out var messageProp) &&
                        messageProp.TryGetProperty("content", out var contentProp))
                    {
                        string aiReply = contentProp.GetString() ?? "Cevap üretilemedi.";
                        return Ok(new { Reply = aiReply });
                    }

                    return Ok(new { Reply = "Cevap anlaşılamadı." });
                }
                else
                {
                    return StatusCode(500, new { Reply = "Ollama sunucusuna bağlanılamadı." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Reply = "Bağlantı hatası: " + ex.Message });
            }
        }
    }
}