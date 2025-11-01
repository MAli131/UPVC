# UPVC Website Assets Structure

## مجلد wwwroot

### images/
- **home/** - صور الصفحة الرئيسية
  - hero-image.jpg - الصورة الرئيسية في Hero Section
  
- **about/** - صور صفحة من نحن
  - company-building.jpg - صورة مبنى الشركة
  - ceo.jpg - صورة المدير التنفيذي
  - technical-manager.jpg - صورة المدير الفني
  - sales-manager.jpg - صورة مدير المبيعات
  - why-choose-us.jpg - صورة لماذا تختارنا
  
- **product/** - صور المنتجات
  - windows.jpg - صورة النوافذ
  - doors.jpg - صورة الأبواب
  - facades.jpg - صورة الواجهات
  - sliding-windows.jpg - النوافذ المنزلقة
  - casement-windows.jpg - نوافذ كازمنت
  - tilt-turn-windows.jpg - نوافذ تيلت أند تورن
  - entrance-doors.jpg - أبواب المداخل
  - sliding-doors.jpg - الأبواب المنزلقة
  - french-doors.jpg - الأبواب الفرنسية
  - curtain-wall.jpg - الجدران الستائرية
  - structural-glazing.jpg - التزجيج الهيكلي
  - handles-locks.jpg - المقابض والأقفال
  
- **contact/** - صور صفحة التواصل
  - contact-hero.jpg - صورة رئيسية للتواصل
  
- **favicon/** - أيقونات الموقع
  - favicon.ico - أيقونة الموقع

### css/
- **site.css** - ملف CSS الأساسي لـ ASP.NET Core
- **style.css** - ملف CSS المخصص (سيتم استبداله بالتصميم الخارجي)

### js/
- **site.js** - ملفات JavaScript المخصصة

### lib/
- مكتبات خارجية (Bootstrap, jQuery, إلخ)

### uploads/
- مجلد لرفع الملفات من المستخدمين

### assets/
- ملفات إضافية حسب التصميم الخارجي

## ملاحظات مهمة:
1. يتم استخدام Bootstrap RTL للدعم العربي
2. تم إعداد Google Fonts (Cairo) للنصوص العربية
3. جميع الصفحات تدعم الاتجاه من اليمين لليسار (RTL)
4. تم إنشاء Models و Controllers جاهزة للاستخدام
5. يمكن إضافة التصميم الخارجي مباشرة في مجلد wwwroot