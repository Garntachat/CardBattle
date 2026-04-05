# วิธีเชื่อมใน Scene

## Script ทั้งหมด
- `ThinkingRate.cs` — ควบคุม slow-mo และ gauge
- `EnemyDetection.cs` — detect ศัตรูเข้า/ออกรัศมี
- `SlowMoEffect.cs` — vignette effect ตอน slow-mo
- `SlowMoSound.cs` — เสียง whoosh และ tick ตอน slow-mo
- `PlayerAnimationController.cs` — trigger animation ของ player

---

## วิธีติดตั้งใน Scene

### 1. สร้าง Empty GameObject ตั้งชื่อ `ThinkingRateManager`
แปะ script เหล่านี้:
- `ThinkingRate`
- `EnemyDetection`
- `SlowMoEffect`
- `SlowMoSound`

### 2. เชื่อม Inspector ของ `ThinkingRateManager`

| ช่อง | ลากอะไรมาวาง |
|---|---|
| EnemyDetection → Enemy | GameObject ของศัตรู |
| EnemyDetection → Thinking Rate | ThinkingRateManager ตัวเอง |
| ThinkingRate → Slow Mo Effect | ThinkingRateManager ตัวเอง |
| ThinkingRate → Slow Mo Sound | ThinkingRateManager ตัวเอง |
| SlowMoEffect → Global Volume | Global Volume ใน Scene |
| SlowMoSound → Whoosh Clip | Sounds/Slowdown.wav |
| SlowMoSound → Whoosh Reverse Clip | Sounds/Slowdown Reverse.wav |
| SlowMoSound → Tick Clip | Sounds/Clock Ticking.wav |

### 3. แปะ `PlayerAnimationController` ที่ Player model
- Animator Controller ต้องใช้ `PlayerAnimator` (อยู่ใน Animations/)
- Trigger ที่ใช้: `DoAttack`, `DoBlock`, `DoDodge`

---

## วิธีเรียกใช้จาก Card System (เดซี่)

```csharp
// หา component จาก Player object
PlayerAnimationController playerAnim = player.GetComponent<PlayerAnimationController>();

// เรียกตาม action การ์ด
playerAnim.PlayAttack();
playerAnim.PlayBlock();
playerAnim.PlayDodge();
```

---

## ปรับค่าได้ใน Inspector

| ค่า | อยู่ที่ | ความหมาย |
|---|---|---|
| Slow Scale | ThinkingRate | ความช้าของ slow-mo (0.25 = ช้า 4x) |
| Recharge Rate | ThinkingRate | gauge เพิ่มเร็วแค่ไหน |
| Detection Radius | EnemyDetection | รัศมี detect ศัตรู |
| Vignette Intensity On | SlowMoEffect | ความมืดขอบจอตอน slow-mo |
| Fade Out Duration | SlowMoEffect | เวลา fade ออก (วินาที) |
