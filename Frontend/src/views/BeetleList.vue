<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import ConfirmModal from '../components/ConfirmModal.vue'

const beetles = ref([])
const router = useRouter()
const showDeleteModal = ref(false)
const beetleToDelete = ref<string | null>(null)

const fetchBeetles = async () => {
  try {
    const response = await axios.get('/api/Beetles')
    beetles.value = response.data
  } catch (error) {
    console.error('Error fetching beetles:', error)
  }
}

const confirmDelete = (id: string) => {
  beetleToDelete.value = id
  showDeleteModal.value = true
}

const executeDelete = async () => {
  if (!beetleToDelete.value) return
  try {
    await axios.delete(`/api/Beetles/${beetleToDelete.value}`)
    showDeleteModal.value = false
    beetleToDelete.value = null
    fetchBeetles()
  } catch (error) {
    console.error('Error deleting beetle:', error)
  }
}

onMounted(() => {
  fetchBeetles()
})

const getStageName = (stage: number) => {
  const stages = ['卵', '幼虫', '蛹', '成虫']
  return stages[stage] || '不明'
}

const getGenderName = (gender: number) => {
  const genders = ['不明', 'オス', 'メス']
  return genders[gender] || '不明'
}

const getGenderBadgeClass = (gender: number) => {
  if (gender === 1) return 'bg-primary bg-opacity-10 text-primary'
  if (gender === 2) return 'bg-danger bg-opacity-10 text-danger'
  return 'bg-secondary bg-opacity-10 text-secondary'
}
</script>

<template>
  <div>
    <h2 class="mb-4 text-secondary border-bottom pb-2">生体・幼虫管理</h2>
    
    <div class="card shadow-sm border-0">
      <div class="card-header bg-white border-bottom pt-3 pb-3 d-flex justify-content-between align-items-center">
        <h5 class="m-0 text-muted">登録データ一覧</h5>
        <router-link to="/beetle/new" class="btn btn-success fw-bold">＋ 新規登録</router-link>
      </div>
      <div class="card-body p-0">
        <!-- Desktop Table -->
        <div class="table-responsive d-none d-md-block">
          <table class="table table-hover align-middle m-0 text-nowrap" style="cursor: pointer;">
            <thead class="table-light">
              <tr>
                <th class="ps-4">管理名</th>
                <th>種類</th>
                <th>ステージ</th>
                <th>性別</th>
                <th>サイズ</th>
                <th class="pe-4 text-end">アクション</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="beetle in beetles" :key="beetle.id" @click="router.push(`/beetle/${beetle.id}`)" class="clickable-row">
                <td class="ps-4 fw-bold">{{ beetle.name || '名称未設定' }}</td>
                <td>{{ beetle.species }}</td>
                <td><span class="badge bg-secondary">{{ getStageName(beetle.stage) }}</span></td>
                <td><span class="badge px-2 py-1" :class="getGenderBadgeClass(beetle.gender)">{{ getGenderName(beetle.gender) }}</span></td>
                <td>{{ beetle.size ? `${beetle.size} mm` : '-' }}</td>
                <td class="pe-4 text-end">
                  <BaseButton class="btn btn-sm btn-outline-danger" @click.stop="confirmDelete(beetle.id)">削除</BaseButton>
                  <span class="ms-3 text-muted">＞</span>
                </td>
              </tr>
              <tr v-if="beetles.length === 0">
                <td colspan="6" class="text-center text-muted py-5">登録されているデータがありません</td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Mobile Cards -->
        <div class="d-md-none p-3 bg-light">
          <div v-for="beetle in beetles" :key="beetle.id" class="card shadow-sm mb-3 border-0" @click="router.push(`/beetle/${beetle.id}`)">
            <div class="card-body">
              <div class="d-flex justify-content-between align-items-center mb-2 border-bottom pb-2">
                <h6 class="fw-bold m-0 text-dark">{{ beetle.name || '名称未設定' }}</h6>
                <div>
                  <span class="badge bg-secondary me-2">{{ getStageName(beetle.stage) }}</span>
                  <BaseButton class="btn btn-sm btn-outline-danger px-2 py-0" @click.stop="confirmDelete(beetle.id)">削除</BaseButton>
                </div>
              </div>
              <div class="row text-muted small gy-2">
                <div class="col-6"><strong>種類:</strong> {{ beetle.species }}</div>
                <div class="col-6">
                  <strong>性別:</strong> <span class="badge px-2 py-1" :class="getGenderBadgeClass(beetle.gender)">{{ getGenderName(beetle.gender) }}</span>
                </div>
                <div class="col-12"><strong>サイズ:</strong> {{ beetle.size ? `${beetle.size} mm` : '-' }}</div>
              </div>
            </div>
          </div>
          <div v-if="beetles.length === 0" class="text-center text-muted py-5">
            登録されているデータがありません
          </div>
        </div>
      </div>
    </div>

    <!-- 削除確認モーダル コンポーネント -->
    <ConfirmModal 
      :show="showDeleteModal" 
      title="生体データの削除" 
      message="このデータを完全に削除します。交換記録などもすべて消去されます。よろしいですか？" 
      confirmText="削除する"
      confirmVariant="danger"
      @close="showDeleteModal = false" 
      @confirm="executeDelete" 
    />
  </div>
</template>

<style scoped>
.clickable-row:hover {
  background-color: #f8f9fa;
}
</style>
