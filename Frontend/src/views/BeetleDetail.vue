<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'
import ConfirmModal from '../components/ConfirmModal.vue'

const route = useRoute()
const router = useRouter()

const beetle = ref(null)
const records = ref([])
const errorMessage = ref('')
const showDeleteModal = ref(false)
const recordToDelete = ref<string | null>(null)

const newRecord = ref({
  exchangeDate: new Date().toISOString().split('T')[0],
  bottleType: '菌糸ビン',
  capacity: 800,
  weight: null,
  headWidth: null,
  notes: ''
})

const fetchBeetleDetail = async () => {
  try {
    const res = await axios.get(`/api/Beetles/${route.params.id}`)
    beetle.value = res.data
  } catch (error) {
    console.error(error)
  }
}

const fetchBottleRecords = async () => {
  try {
    const res = await axios.get(`/api/BottleRecords/beetle/${route.params.id}`)
    records.value = res.data
  } catch (error) {
    console.error(error)
  }
}

const addBottleRecord = async () => {
  errorMessage.value = ''
  try {
    await axios.post('/api/BottleRecords', { ...newRecord.value, beetleId: route.params.id })
    newRecord.value = { exchangeDate: new Date().toISOString().split('T')[0], bottleType: '菌糸ビン', capacity: 800, weight: null, headWidth: null, notes: '' }
    fetchBottleRecords()
  } catch (error) {
    console.error(error)
    errorMessage.value = '記録の追加に失敗しました。'
  }
}

const confirmDelete = (id: string) => {
  recordToDelete.value = id
  showDeleteModal.value = true
}

const executeDelete = async () => {
  if (!recordToDelete.value) return
  errorMessage.value = ''
  try {
    await axios.delete(`/api/BottleRecords/${recordToDelete.value}`)
    showDeleteModal.value = false
    recordToDelete.value = null
    fetchBottleRecords()
  } catch (error) {
    console.error(error)
    errorMessage.value = '記録の削除に失敗しました。'
    showDeleteModal.value = false
  }
}

onMounted(() => {
  fetchBeetleDetail()
  fetchBottleRecords()
})

const getStageName = (stage: number) => ['卵', '幼虫', '蛹', '成虫'][stage] || '不明'
const getGenderName = (gender: number) => ['不明', 'オス', 'メス'][gender] || '不明'
</script>

<template>
  <div v-if="beetle">
    <div class="d-flex justify-content-between align-items-center mb-4 border-bottom pb-2">
      <h2 class="text-secondary m-0">個体詳細: <span class="text-dark fw-bold">{{ beetle.name || '名称未設定' }}</span></h2>
      <BaseButton class="btn btn-outline-secondary" @click="router.push('/beetles')">一覧へ戻る</BaseButton>
    </div>

    <!-- 生体情報カード -->
    <div class="card shadow-sm border-0 mb-4">
      <div class="card-header bg-white border-bottom pt-3 pb-3">
        <h5 class="m-0 text-muted fw-bold">基本情報</h5>
      </div>
      <!-- Desktop Basic Info Table -->
      <div class="card-body p-0 table-responsive d-none d-md-block">
        <table class="table table-bordered m-0 align-middle text-nowrap">
          <tbody>
            <tr>
              <th class="bg-light" style="width: 15%;">種類</th><td style="width: 35%;">{{ beetle.species }}</td>
              <th class="bg-light" style="width: 15%;">産地</th><td style="width: 35%;">{{ beetle.locality || '-' }}</td>
            </tr>
            <tr>
              <th class="bg-light">累代</th><td>{{ beetle.generation || '-' }}</td>
              <th class="bg-light">ステージ</th><td><span class="badge bg-secondary">{{ getStageName(beetle.stage) }}</span></td>
            </tr>
            <tr>
              <th class="bg-light">性別</th><td>{{ getGenderName(beetle.gender) }}</td>
              <th class="bg-light">サイズ</th><td>{{ beetle.size ? `${beetle.size} mm` : '-' }}</td>
            </tr>
            <tr>
              <th class="bg-light">オス親 (Sire)</th>
              <td>
                <router-link v-if="beetle.sireId" :to="`/beetle/${beetle.sireId}`" class="text-decoration-none fw-bold text-primary">{{ beetle.sire?.name || beetle.sire?.species || '不明' }}</router-link>
                <span v-else class="text-muted">不明</span>
              </td>
              <th class="bg-light">メス親 (Dam)</th>
              <td>
                <router-link v-if="beetle.damId" :to="`/beetle/${beetle.damId}`" class="text-decoration-none fw-bold text-danger">{{ beetle.dam?.name || beetle.dam?.species || '不明' }}</router-link>
                <span v-else class="text-muted">不明</span>
              </td>
            </tr>
            <tr>
              <th class="bg-light">孵化日</th><td>{{ beetle.hatchDate ? beetle.hatchDate.split('T')[0] : '-' }}</td>
              <th class="bg-light">羽化日</th><td>{{ beetle.emergenceDate ? beetle.emergenceDate.split('T')[0] : '-' }}</td>
            </tr>
            <tr>
              <th class="bg-light">メモ</th><td colspan="3" class="text-wrap">{{ beetle.notes || '-' }}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Mobile Basic Info -->
      <div class="card-body p-3 d-md-none">
        <div class="row gy-3">
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">種類</div>
            <div class="fw-bold">{{ beetle.species }}</div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">産地</div>
            <div class="fw-bold">{{ beetle.locality || '-' }}</div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">累代</div>
            <div class="fw-bold">{{ beetle.generation || '-' }}</div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">ステージ / 性別</div>
            <div class="fw-bold">
              <span class="badge bg-secondary me-1">{{ getStageName(beetle.stage) }}</span>
              <span>{{ getGenderName(beetle.gender) }}</span>
            </div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">サイズ</div>
            <div class="fw-bold">{{ beetle.size ? `${beetle.size} mm` : '-' }}</div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">親情報</div>
            <div class="small">
              <span class="text-primary">♂:</span> {{ beetle.sire?.name || '不明' }}<br>
              <span class="text-danger">♀:</span> {{ beetle.dam?.name || '不明' }}
            </div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">孵化日</div>
            <div class="fw-bold">{{ beetle.hatchDate ? beetle.hatchDate.split('T')[0] : '-' }}</div>
          </div>
          <div class="col-6 border-bottom pb-2">
            <div class="text-muted small mb-1">羽化日</div>
            <div class="fw-bold">{{ beetle.emergenceDate ? beetle.emergenceDate.split('T')[0] : '-' }}</div>
          </div>
          <div class="col-12">
            <div class="text-muted small mb-1">メモ</div>
            <div>{{ beetle.notes || '-' }}</div>
          </div>
        </div>
      </div>
    </div>

    <!-- エラーメッセージ表示 -->
    <div v-if="errorMessage" class="alert alert-danger shadow-sm border-0">
      {{ errorMessage }}
    </div>

    <!-- 菌糸ビン交換記録 -->
    <h3 class="mb-3 text-secondary">ビン・マット交換記録</h3>
    
    <div class="card shadow-sm border-0 mb-4 bg-light">
      <div class="card-body p-3">
        <form @submit.prevent="addBottleRecord" class="row gx-2 gy-2 align-items-end">
          <div class="col-md-2">
            <label class="form-label small fw-bold">交換日</label>
            <BaseInput type="date" class="form-control form-control-sm" v-model="newRecord.exchangeDate" required />
          </div>
          <div class="col-md-2">
            <label class="form-label small fw-bold">種類</label>
            <BaseInput type="text" class="form-control form-control-sm" v-model="newRecord.bottleType" placeholder="菌糸, マット等" required />
          </div>
          <div class="col-md-1">
            <label class="form-label small fw-bold">容量(cc)</label>
            <BaseInput type="number" class="form-control form-control-sm" v-model="newRecord.capacity" required />
          </div>
          <div class="col-md-1">
            <label class="form-label small fw-bold">体重(g)</label>
            <BaseInput type="number" step="0.1" class="form-control form-control-sm" v-model="newRecord.weight" />
          </div>
          <div class="col-md-1">
            <label class="form-label small fw-bold">頭幅(mm)</label>
            <BaseInput type="number" step="0.1" class="form-control form-control-sm" v-model="newRecord.headWidth" />
          </div>
          <div class="col-md-4">
            <label class="form-label small fw-bold">メモ</label>
            <BaseInput type="text" class="form-control form-control-sm" v-model="newRecord.notes" placeholder="銘柄など" />
          </div>
          <div class="col-md-1 d-grid">
            <BaseButton type="submit" class="btn btn-primary btn-sm fw-bold">追加</BaseButton>
          </div>
        </form>
      </div>
    </div>

    <div class="card shadow-sm border-0">
      <!-- Desktop Table -->
      <div class="table-responsive d-none d-md-block">
        <table class="table table-hover align-middle m-0 text-nowrap">
          <thead class="table-light">
            <tr>
              <th class="ps-3">交換日</th>
              <th>種類 / 容量</th>
              <th>体重</th>
              <th>頭幅</th>
              <th>メモ</th>
              <th class="pe-3 text-end">操作</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="record in records" :key="record.id">
              <td class="ps-3 fw-bold">{{ record.exchangeDate.split('T')[0] }}</td>
              <td><span class="badge bg-secondary">{{ record.bottleType }}</span> {{ record.capacity }}cc</td>
              <td class="fw-bold text-success">{{ record.weight ? `${record.weight} g` : '-' }}</td>
              <td>{{ record.headWidth ? `${record.headWidth} mm` : '-' }}</td>
              <td class="text-muted">{{ record.notes || '-' }}</td>
              <td class="pe-3 text-end">
                <BaseButton class="btn btn-sm btn-outline-danger" @click="confirmDelete(record.id)">削除</BaseButton>
              </td>
            </tr>
            <tr v-if="records.length === 0">
              <td colspan="6" class="text-center text-muted py-4">交換記録がありません</td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Mobile Cards -->
      <div class="d-md-none p-3 bg-light">
        <div v-for="record in records" :key="record.id" class="card shadow-sm mb-3 border-0">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center border-bottom pb-2 mb-2">
              <span class="fw-bold text-dark">{{ record.exchangeDate.split('T')[0] }}</span>
              <BaseButton class="btn btn-sm btn-outline-danger px-2 py-0" @click="confirmDelete(record.id)">削除</BaseButton>
            </div>
            <div class="row text-muted small gy-2">
              <div class="col-6">
                <strong>種類:</strong> <span class="badge bg-secondary">{{ record.bottleType }}</span>
              </div>
              <div class="col-6"><strong>容量:</strong> {{ record.capacity }}cc</div>
              <div class="col-6"><strong>体重:</strong> <span class="text-success fw-bold">{{ record.weight ? `${record.weight} g` : '-' }}</span></div>
              <div class="col-6"><strong>頭幅:</strong> {{ record.headWidth ? `${record.headWidth} mm` : '-' }}</div>
              <div class="col-12 border-top pt-2 mt-2"><strong>メモ:</strong> {{ record.notes || '-' }}</div>
            </div>
          </div>
        </div>
        <div v-if="records.length === 0" class="text-center text-muted py-4">
          交換記録がありません
        </div>
      </div>
    </div>

    <!-- 削除確認モーダル コンポーネント -->
    <ConfirmModal 
      :show="showDeleteModal" 
      title="記録の削除" 
      message="この交換記録を完全に削除します。よろしいですか？" 
      confirmText="削除する"
      confirmVariant="danger"
      @close="showDeleteModal = false" 
      @confirm="executeDelete" 
    />
  </div>
  <div v-else class="text-center p-5">
    <div class="spinner-border text-success" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
  </div>
</template>
