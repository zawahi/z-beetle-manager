<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import StatCard from '../components/StatCard.vue'

const stats = ref(null)
const router = useRouter()

const fetchStats = async () => {
  try {
    const response = await axios.get('/api/Dashboard/stats')
    stats.value = response.data
  } catch (error) {
    console.error('Error fetching dashboard stats:', error)
  }
}

onMounted(() => {
  fetchStats()
})
</script>

<template>
  <div>
    <h2 class="mb-4 text-secondary border-bottom pb-2">ダッシュボード</h2>
    
    <div v-if="stats" class="row g-4 mb-5">
      <StatCard title="総飼育数" :value="stats.totalBeetles" link="/beetles" borderColorClass="border-primary" />
      <StatCard title="幼虫" :value="stats.larvaeCount" link="/beetles" borderColorClass="border-info" />
      <StatCard title="成虫" :value="stats.adultCount" link="/beetles" borderColorClass="border-success" />
      <StatCard title="進行中のペアリング" :value="stats.breedingActiveCount" link="/breeding" borderColorClass="border-danger" />
    </div>

    <div v-if="stats && stats.bottleAlerts.length > 0" class="card shadow-sm border-0 border-start border-danger border-5 mb-4">
      <div class="card-header bg-white border-bottom-0 pt-4 pb-0">
        <h5 class="text-danger fw-bold"><i class="bi bi-exclamation-triangle-fill"></i> ボトル交換アラート（3ヶ月経過）</h5>
      </div>
      <div class="card-body p-0 mt-2">
        <!-- Desktop Table -->
        <div class="table-responsive d-none d-md-block">
          <table class="table table-hover align-middle mb-0 text-nowrap">
            <thead class="table-light">
              <tr>
                <th class="ps-4">管理名</th>
                <th>前回交換日</th>
                <th>経過日数</th>
                <th class="pe-4 text-end">アクション</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="alert in stats.bottleAlerts" :key="alert.beetleId">
                <td class="ps-4 fw-bold">{{ alert.beetleName }}</td>
                <td>{{ alert.lastExchangeDate.split('T')[0] }}</td>
                <td class="text-danger fw-bold">{{ alert.daysSinceExchange }} 日</td>
                <td class="pe-4 text-end">
                  <BaseButton class="btn btn-sm btn-outline-danger" @click="router.push(`/beetle/${alert.beetleId}`)">詳細を確認</BaseButton>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Mobile Cards -->
        <div class="d-md-none p-3 bg-light">
          <div v-for="alert in stats.bottleAlerts" :key="alert.beetleId" class="card shadow-sm border-danger border-opacity-50 mb-3">
            <div class="card-body">
              <div class="d-flex justify-content-between align-items-center mb-2">
                <h6 class="fw-bold m-0 text-dark">{{ alert.beetleName }}</h6>
                <span class="text-danger fw-bold">{{ alert.daysSinceExchange }} 日経過</span>
              </div>
              <div class="text-muted small mb-3">
                <strong>前回交換日:</strong> {{ alert.lastExchangeDate.split('T')[0] }}
              </div>
              <BaseButton class="btn btn-outline-danger w-100 fw-bold" @click="router.push(`/beetle/${alert.beetleId}`)">詳細を確認</BaseButton>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.clickable-card {
  cursor: pointer;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
.clickable-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 .5rem 1rem rgba(0,0,0,.15)!important;
}
</style>
