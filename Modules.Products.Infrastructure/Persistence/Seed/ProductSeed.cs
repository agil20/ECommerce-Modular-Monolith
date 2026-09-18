namespace Modules.Products.Infrastructure.Persistence.Seed;

internal sealed record SeedProduct(int Id, string Name, double Price, int CategoryId, string Description);

internal static class ProductSeed
{
    public static readonly DateTimeOffset SeedDate = new(2026, 8, 17, 0, 0, 0, TimeSpan.Zero);

    // CategoryId values must match the Categories module seed (CategoryConfiguration).
    // Product ids are grouped by category: 1xx = category 1, 2xx = category 2, ...
    public static readonly IReadOnlyList<SeedProduct> Items =
    [
        // 1 — Elektronika
        new(101, "Apple iPhone 15 Pro", 2799.00, 1,
            "6.1 düymlük Super Retina XDR ekran, A17 Pro çip, 256 GB yaddaş və titan korpus."),
        new(102, "Samsung Galaxy S24 Ultra", 2599.00, 1,
            "6.8 düymlük Dynamic AMOLED ekran, 200 MP kamera, S Pen dəstəyi və 512 GB yaddaş."),
        new(103, "Simsiz Qulaqlıq AirPods Pro 2", 499.00, 1,
            "Aktiv səs-küy söndürmə, məkan səsi, MagSafe şarj qutusu ilə 30 saata qədər işləmə."),
        new(104, "Samsung 55\" 4K Smart TV", 1299.00, 1,
            "55 düymlük Crystal UHD ekran, HDR10+, Tizen əməliyyat sistemi, Wi-Fi və Bluetooth."),

        // 2 — Kompüter və Aksesuarlar
        new(201, "Noutbuk Asus ROG Strix G16", 3499.00, 2,
            "Intel Core i9, RTX 4070, 16 GB RAM, 1 TB SSD və 165 Hz ekran — oyun və montaj üçün."),
        new(202, "MacBook Air M3", 2699.00, 2,
            "13.6 düymlük Liquid Retina ekran, Apple M3 çip, 8 GB RAM, 256 GB SSD, 18 saat batareya."),
        new(203, "Mexaniki Klaviatura Logitech G Pro", 229.00, 2,
            "GX Blue açarları, RGB işıqlandırma, çıxarıla bilən kabel və kompakt TKL dizayn."),
        new(204, "Simsiz Siçan Logitech MX Master 3S", 189.00, 2,
            "8000 DPI sensor, səssiz düymələr, 3 cihaza qoşulma və USB-C ilə sürətli şarj."),

        // 3 — Geyim
        new(301, "Kişi Qış Gödəkcəsi", 189.50, 3,
            "Su keçirməyən parça, isti astar və kapüşon; -20°C-yə qədər istiliyi saxlayır."),
        new(302, "Qadın Yay Donu", 85.00, 3,
            "Yüngül pambıq parça, çiçək naxışı, diz uzunluğunda — gündəlik geyim və tətil üçün."),
        new(303, "Kişi Klassik Köynək", 59.90, 3,
            "100% pambıq, slim fit kəsim, ütü tələb etməyən parça, ağ rəng."),
        new(304, "Unisex Hudi", 74.00, 3,
            "Yumşaq flis daxili hissə, kenquru cibi, oversize kəsim, 5 rəng seçimi."),

        // 4 — Ayaqqabı
        new(401, "Nike Air Max 270", 279.00, 4,
            "Böyük Air yastıqlı daban və nəfəs alan tor üst hissə — gün boyu rahatlıq."),
        new(402, "Adidas Ultraboost Light", 329.00, 4,
            "Boost köpük altlıq və Primeknit üst hissə — qaçış və gündəlik istifadə üçün."),
        new(403, "Kişi Dəri Çəkmə", 219.00, 4,
            "Təbii dəri, sürüşməyən altlıq və isti astar — qış mövsümü üçün."),
        new(404, "Qadın Klassik Tufli", 139.00, 4,
            "Lak dəri, 7 sm daban, yumşaq içlik — ofis və tədbirlər üçün."),

        // 5 — Ev və Mebel
        new(501, "Ortopedik Matras 160x200", 649.00, 5,
            "Müstəqil yay bloku, memory foam üst qat, orta sərtlik və 10 il zəmanət."),
        new(502, "İş Masası", 249.00, 5,
            "120x60 sm iş səthi, metal ayaqlar və kabel kanalı — ev ofisi üçün."),
        new(503, "Ergonomik Ofis Kreslosu", 389.00, 5,
            "Bel dəstəyi, tənzimlənən qoltuqaltı və hündürlük, nəfəs alan tor arxalıq."),
        new(504, "Üçnəfərlik Divan", 1199.00, 5,
            "Velvet üzlük, yumşaq oturacaq, bərk ağac karkas, boz rəng."),

        // 6 — Mətbəx
        new(601, "Tefal Qeyri-yapışqan Tava Dəsti", 159.00, 6,
            "3 tava (20/24/28 sm), titan örtük, induksiya daxil bütün plitələrə uyğun."),
        new(602, "Philips Airfryer XL", 349.00, 6,
            "6.2 litr tutum, yağsız qızartma, 7 hazır proqram və asan təmizlənən səbət."),
        new(603, "De'Longhi Qəhvə Maşını", 899.00, 6,
            "Avtomatik espresso, daxili dəyirman, süd köpükləndirici və 15 bar təzyiq."),
        new(604, "Bıçaq Dəsti (6 ədəd)", 119.00, 6,
            "Paslanmayan polad, ergonomik dəstək və taxta altlıq."),

        // 7 — İdman və Əyləncə
        new(701, "Qaçış Trenajoru", 1450.00, 7,
            "3 HP mühərrik, 18 km/saat sürət, 12 hazır proqram və qatlana bilən konstruksiya."),
        new(702, "Futbol Topu Nike Strike", 65.00, 7,
            "FIFA Quality standartı, 5 ölçü, möhkəm rezin kamera — bütün meydançalar üçün."),
        new(703, "Yoqa Xalçası", 45.00, 7,
            "6 mm qalınlıq, sürüşməyən səth, TPE material və daşıma kəməri."),
        new(704, "Tənzimlənən Hantel Dəsti 2x20 kq", 299.00, 7,
            "Tez dəyişən disklər, xrom qolluqlar və saxlama qutusu — ev məşqləri üçün."),

        // 8 — Kitablar
        new(801, "Clean Code — Robert C. Martin", 49.00, 8,
            "Təmiz, oxunaqlı və asan saxlanılan kod yazmaq haqqında klassik kitab."),
        new(802, "Designing Data-Intensive Applications", 69.00, 8,
            "Paylanmış sistemlər, verilənlər bazaları və miqyaslama haqqında dərin bələdçi."),
        new(803, "Səfillər — Viktor Hüqo", 29.00, 8,
            "Dünya ədəbiyyatının şah əsərlərindən biri, Azərbaycan dilində tam nəşr."),
        new(804, "Atomik Vərdişlər — Ceyms Klir", 25.00, 8,
            "Kiçik vərdişlərlə böyük nəticələr əldə etməyin praktik yolları.")
    ];
}
