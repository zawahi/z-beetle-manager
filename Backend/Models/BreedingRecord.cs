using System;

namespace KuwagataApp.Backend.Models
{
    public class BreedingRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty; // ユーザーごと
        
        public string MaleName { get; set; } = string.Empty; // オス親 (手入力)
        public string FemaleName { get; set; } = string.Empty; // メス親 (手入力)

        public DateTime StartDate { get; set; } = DateTime.UtcNow; // ペアリング開始日 (同居開始)
        public DateTime? EndDate { get; set; } // ペアリング終了日
        
        public DateTime? SetDate { get; set; } // 産卵セット投入日
        public DateTime? AllocationDate { get; set; } // 割り出し日
        
        public int? EggCount { get; set; } // 採卵数
        public int? LarvaCount { get; set; } // 幼虫数
        
        public string? Notes { get; set; } // 結果メモ
    }
}
