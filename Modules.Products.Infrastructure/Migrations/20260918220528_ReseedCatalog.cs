using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modules.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReseedCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Products",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                schema: "Products",
                table: "ProductDescriptions",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 101, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "6.1 düymlük Super Retina XDR ekran, A17 Pro çip, 256 GB yaddaş və titan korpus.", false, null },
                    { 102, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "6.8 düymlük Dynamic AMOLED ekran, 200 MP kamera, S Pen dəstəyi və 512 GB yaddaş.", false, null },
                    { 103, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Aktiv səs-küy söndürmə, məkan səsi, MagSafe şarj qutusu ilə 30 saata qədər işləmə.", false, null },
                    { 104, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "55 düymlük Crystal UHD ekran, HDR10+, Tizen əməliyyat sistemi, Wi-Fi və Bluetooth.", false, null }
                });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Apple iPhone 15 Pro", 2799.0 });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Samsung Galaxy S24 Ultra", 2599.0 });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Simsiz Qulaqlıq AirPods Pro 2", 499.0 });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Samsung 55\" 4K Smart TV", 1299.0 });

            migrationBuilder.InsertData(
                schema: "Products",
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsDeleted", "IsVip", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 201, 2, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Noutbuk Asus ROG Strix G16", 3499.0, null },
                    { 202, 2, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "MacBook Air M3", 2699.0, null },
                    { 203, 2, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Mexaniki Klaviatura Logitech G Pro", 229.0, null },
                    { 204, 2, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Simsiz Siçan Logitech MX Master 3S", 189.0, null },
                    { 301, 3, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Kişi Qış Gödəkcəsi", 189.5, null },
                    { 302, 3, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Qadın Yay Donu", 85.0, null },
                    { 303, 3, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Kişi Klassik Köynək", 59.899999999999999, null },
                    { 304, 3, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Unisex Hudi", 74.0, null },
                    { 401, 4, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Nike Air Max 270", 279.0, null },
                    { 402, 4, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Adidas Ultraboost Light", 329.0, null },
                    { 403, 4, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Kişi Dəri Çəkmə", 219.0, null },
                    { 404, 4, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Qadın Klassik Tufli", 139.0, null },
                    { 501, 5, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Ortopedik Matras 160x200", 649.0, null },
                    { 502, 5, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "İş Masası", 249.0, null },
                    { 503, 5, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Ergonomik Ofis Kreslosu", 389.0, null },
                    { 504, 5, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Üçnəfərlik Divan", 1199.0, null },
                    { 601, 6, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Tefal Qeyri-yapışqan Tava Dəsti", 159.0, null },
                    { 602, 6, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Philips Airfryer XL", 349.0, null },
                    { 603, 6, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "De'Longhi Qəhvə Maşını", 899.0, null },
                    { 604, 6, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Bıçaq Dəsti (6 ədəd)", 119.0, null },
                    { 701, 7, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Qaçış Trenajoru", 1450.0, null },
                    { 702, 7, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Futbol Topu Nike Strike", 65.0, null },
                    { 703, 7, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Yoqa Xalçası", 45.0, null },
                    { 704, 7, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Tənzimlənən Hantel Dəsti 2x20 kq", 299.0, null },
                    { 801, 8, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Clean Code — Robert C. Martin", 49.0, null },
                    { 802, 8, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Designing Data-Intensive Applications", 69.0, null },
                    { 803, 8, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Səfillər — Viktor Hüqo", 29.0, null },
                    { 804, 8, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Atomik Vərdişlər — Ceyms Klir", 25.0, null }
                });

            migrationBuilder.InsertData(
                schema: "Products",
                table: "ProductDescriptions",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 201, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Intel Core i9, RTX 4070, 16 GB RAM, 1 TB SSD və 165 Hz ekran — oyun və montaj üçün.", false, null },
                    { 202, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "13.6 düymlük Liquid Retina ekran, Apple M3 çip, 8 GB RAM, 256 GB SSD, 18 saat batareya.", false, null },
                    { 203, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "GX Blue açarları, RGB işıqlandırma, çıxarıla bilən kabel və kompakt TKL dizayn.", false, null },
                    { 204, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "8000 DPI sensor, səssiz düymələr, 3 cihaza qoşulma və USB-C ilə sürətli şarj.", false, null },
                    { 301, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Su keçirməyən parça, isti astar və kapüşon; -20°C-yə qədər istiliyi saxlayır.", false, null },
                    { 302, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Yüngül pambıq parça, çiçək naxışı, diz uzunluğunda — gündəlik geyim və tətil üçün.", false, null },
                    { 303, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "100% pambıq, slim fit kəsim, ütü tələb etməyən parça, ağ rəng.", false, null },
                    { 304, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Yumşaq flis daxili hissə, kenquru cibi, oversize kəsim, 5 rəng seçimi.", false, null },
                    { 401, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Böyük Air yastıqlı daban və nəfəs alan tor üst hissə — gün boyu rahatlıq.", false, null },
                    { 402, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Boost köpük altlıq və Primeknit üst hissə — qaçış və gündəlik istifadə üçün.", false, null },
                    { 403, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Təbii dəri, sürüşməyən altlıq və isti astar — qış mövsümü üçün.", false, null },
                    { 404, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Lak dəri, 7 sm daban, yumşaq içlik — ofis və tədbirlər üçün.", false, null },
                    { 501, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Müstəqil yay bloku, memory foam üst qat, orta sərtlik və 10 il zəmanət.", false, null },
                    { 502, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "120x60 sm iş səthi, metal ayaqlar və kabel kanalı — ev ofisi üçün.", false, null },
                    { 503, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Bel dəstəyi, tənzimlənən qoltuqaltı və hündürlük, nəfəs alan tor arxalıq.", false, null },
                    { 504, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Velvet üzlük, yumşaq oturacaq, bərk ağac karkas, boz rəng.", false, null },
                    { 601, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "3 tava (20/24/28 sm), titan örtük, induksiya daxil bütün plitələrə uyğun.", false, null },
                    { 602, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "6.2 litr tutum, yağsız qızartma, 7 hazır proqram və asan təmizlənən səbət.", false, null },
                    { 603, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Avtomatik espresso, daxili dəyirman, süd köpükləndirici və 15 bar təzyiq.", false, null },
                    { 604, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paslanmayan polad, ergonomik dəstək və taxta altlıq.", false, null },
                    { 701, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "3 HP mühərrik, 18 km/saat sürət, 12 hazır proqram və qatlana bilən konstruksiya.", false, null },
                    { 702, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "FIFA Quality standartı, 5 ölçü, möhkəm rezin kamera — bütün meydançalar üçün.", false, null },
                    { 703, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "6 mm qalınlıq, sürüşməyən səth, TPE material və daşıma kəməri.", false, null },
                    { 704, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tez dəyişən disklər, xrom qolluqlar və saxlama qutusu — ev məşqləri üçün.", false, null },
                    { 801, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Təmiz, oxunaqlı və asan saxlanılan kod yazmaq haqqında klassik kitab.", false, null },
                    { 802, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paylanmış sistemlər, verilənlər bazaları və miqyaslama haqqında dərin bələdçi.", false, null },
                    { 803, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dünya ədəbiyyatının şah əsərlərindən biri, Azərbaycan dilində tam nəşr.", false, null },
                    { 804, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Kiçik vərdişlərlə böyük nəticələr əldə etməyin praktik yolları.", false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "ProductDescriptions",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Products",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Noutbuk Asus ROG", 2499.9899999999998 });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Apple iPhone 15 Pro", 2799.0 });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Simsiz Qulaqlıq AirPods", 450.0 });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Kişi Qış Gödəkcəsi", 120.5 });

            migrationBuilder.InsertData(
                schema: "Products",
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsDeleted", "IsVip", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 105, 3, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Qadın Donu", 85.0, null },
                    { 106, 4, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Ortopedik Matras", 300.0, null },
                    { 107, 4, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "İş Masası", 150.0, null },
                    { 108, 5, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Qaçış Trenajoru", 800.0, null },
                    { 109, 5, new DateTimeOffset(new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Futbol Topu (Nike)", 65.0, null }
                });
        }
    }
}
