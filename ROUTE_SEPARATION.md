ئ#33333




3








هخخههههخهممممممممممممممممممممم
 ةو UPVC System - Route Separation Documentation

## نظام المسارات المنفصلة

### 1. المسارات العامة (Public Routes)
تبدأ جميع المسارات العامة بـ `/` وهي الافتراضية عند بدء التطبيق:

- `/` - الصفحة الرئيسية (Home/Index)
- `/Home/Home2` - الصفحة الرئيسية 2
- `/Home/Home3` - الصفحة الرئيسية 3
- `/Home/Home4` - الصفحة الرئيسية 4
- `/Product` - صفحة المنتجات
- `/Product/Details/{id}` - تفاصيل المنتج
- `/About` - من نحن
- `/Contact` - اتصل بنا

**الحماية:**
- جميع Controllers العامة محمية بـ `[PreventAdminAccess]`
- إذا كان Admin مسجل دخول، سيتم توجيهه تلقائياً للـ Dashboard

### 2. مسارات لوحة التحكم (Admin Routes)
تبدأ جميع مسارات الـ Admin بـ `/Admin/`:

- `/Admin/Account/Login` - تسجيل دخول الإدارة
- `/Admin/Dashboard` - لوحة التحكم الرئيسية
- `/Admin/HomePages` - إدارة الصفحات الرئيسية
- `/Admin/HomePages/Edit/{id}` - تعديل صفحة رئيسية
- `/Admin/CompanyInfo/Edit` - إدارة بيانات الشركة
- `/Admin/SocialMedia` - إدارة حسابات التواصل الاجتماعي
- `/Admin/SocialMedia/Create` - إضافة حساب جديد
- `/Admin/SocialMedia/Edit/{id}` - تعديل حساب
- `/Admin/Products` - إدارة المنتجات (قريباً)
- `/Admin/About` - إدارة صفحة من نحن (قريباً)
- `/Admin/Contact` - إدارة صفحة اتصل بنا (قريباً)

**الحماية:**
- جميع Controllers الإدارية (ماعدا AccountController) محمية بـ `[AdminAuth]`
- عند محاولة الدخول بدون تسجيل، يتم التوجيه لصفحة Login

### 3. قواعد الفصل بين المسارين

#### من الموقع العام إلى لوحة التحكم:
✅ يمكن الوصول لـ `/Admin/Account/Login` من أي مكان
✅ بعد تسجيل الدخول، يتم التوجيه للـ Dashboard

#### من لوحة التحكم إلى الموقع العام:
❌ لا يمكن الوصول للمسارات العامة أثناء تسجيل الدخول كـ Admin
✅ يجب تسجيل الخروج أولاً عبر:
```
POST /Admin/Account/Logout
```
✅ بعد تسجيل الخروج، يمكن الوصول للموقع العام

### 4. بيانات تسجيل الدخول الافتراضية

```
Username: admin
Password: Admin@123
```

### 5. التنفيذ التقني

#### Filters المستخدمة:

**AdminAuthAttribute.cs**
```csharp
// حماية صفحات الإدارة - يمنع الدخول بدون تسجيل دخول
[AdminAuth]
```

**PreventAdminAccessAttribute.cs**
```csharp
// حماية الصفحات العامة - يمنع Admin من الوصول بدون تسجيل خروج
[PreventAdminAccess]
```

#### Session Management:
```csharp
// تسجيل الدخول
HttpContext.Session.SetString("AdminAuthenticated", "true");
HttpContext.Session.SetString("AdminUsername", username);
HttpContext.Session.SetInt32("AdminId", id);

// تسجيل الخروج
HttpContext.Session.Clear();
```

### 6. رسائل المستخدم

- عند تسجيل الخروج من لوحة التحكم: "تم تسجيل الخروج بنجاح. يمكنك الآن الوصول للموقع العام."
- في Dashboard: تنبيه يوضح أنه يجب تسجيل الخروج للوصول للموقع العام

### 7. الأمان

✅ Session-based authentication
✅ فصل تام بين المسارين
✅ عدم إمكانية التنقل بين المسارين بدون تسجيل خروج
✅ كل Controller محمي بالـ Attribute المناسب
✅ جميع كلمات المرور مشفرة بـ BCrypt

---

**آخر تحديث:** November 5, 2025
