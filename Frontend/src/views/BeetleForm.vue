<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'

const router = useRouter()

const males = ref([])
const females = ref([])
const errorMessage = ref('')

const form = ref({
  name: '',
  species: 'オオクワガタ',
  locality: '',
  generation: '',
  stage: 1, 
  gender: 0, 
  size: null,
  hatchDate: null,
  emergenceDate: null,
  sireId: null,
  damId: null,
  notes: ''
})

const isSubmitting = ref(false)

const fetchAdults = async () => {
  try {
    const response = await axios.get('/api/Beetles')
    males.value = response.data.filter(b => b.stage === 3 && b.gender === 1)
    females.value = response.data.filter(b => b.stage === 3 && b.gender === 2)
  } catch (error) {
    console.error('Error fetching beetles:', error)
  }
}

onMounted(() => {
  fetchAdults()
})

const submitForm = async () => {
  isSubmitting.value = true
  errorMessage.value = ''
  try {
    await axios.post('/api/Beetles', form.value)
    router.push('/beetles')
  } catch (error) {
    console.error('Registration failed:', error)
    errorMessage.value = '登録に失敗しました。入力内容を確認してください。'
  } finally {
    isSubmitting.value = false
  }
}
</script>

<template>
  <div>
    <h2 class="mb-4 text-secondary border-bottom pb-2">生体・幼虫の新規登録</h2>
    
    <div v-if="errorMessage" class="alert alert-danger shadow-sm border-0 mb-4">
      {{ errorMessage }}
    </div>

    <div class="card shadow-sm border-0">
      <div class="card-body p-4">
        <form @submit.prevent="submitForm">
          
          <div class="row mb-3">
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">管理名・番号</label>
              <BaseInput type="text" class="form-control" v-model="form.name" placeholder="例: 23年1番" />
            </div>
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">種類</label>
              <BaseInput type="text" class="form-control" v-model="form.species" required />
            </div>
          </div>

          <div class="row mb-3">
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">産地</label>
              <BaseInput type="text" class="form-control" v-model="form.locality" placeholder="例: 能勢" />
            </div>
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">累代</label>
              <BaseInput type="text" class="form-control" v-model="form.generation" placeholder="例: CBF1" />
            </div>
          </div>

          <div class="row mb-3">
            <div class="col-md-4">
              <label class="form-label fw-bold text-secondary">ステージ</label>
              <select class="form-select" v-model.number="form.stage">
                <option :value="0">卵</option>
                <option :value="1">幼虫</option>
                <option :value="2">蛹</option>
                <option :value="3">成虫</option>
              </select>
            </div>
            <div class="col-md-4">
              <label class="form-label fw-bold text-secondary">性別</label>
              <select class="form-select" v-model.number="form.gender">
                <option :value="0">不明</option>
                <option :value="1">オス</option>
                <option :value="2">メス</option>
              </select>
            </div>
            <div class="col-md-4">
              <label class="form-label fw-bold text-secondary">サイズ (mm)</label>
              <BaseInput type="number" step="0.1" class="form-control" v-model="form.size" placeholder="例: 80.5" />
            </div>
          </div>

          <div class="row mb-3">
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">孵化日</label>
              <BaseInput type="date" class="form-control" v-model="form.hatchDate" />
            </div>
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">羽化日</label>
              <BaseInput type="date" class="form-control" v-model="form.emergenceDate" />
            </div>
          </div>

          <div class="row mb-4">
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">オス親 (Sire)</label>
              <select class="form-select" v-model="form.sireId">
                <option :value="null">不明・登録なし</option>
                <option v-for="m in males" :key="m.id" :value="m.id">
                  {{ m.name || m.species }} ({{ m.size ? m.size + 'mm' : 'サイズ不明' }})
                </option>
              </select>
            </div>
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">メス親 (Dam)</label>
              <select class="form-select" v-model="form.damId">
                <option :value="null">不明・登録なし</option>
                <option v-for="f in females" :key="f.id" :value="f.id">
                  {{ f.name || f.species }} ({{ f.size ? f.size + 'mm' : 'サイズ不明' }})
                </option>
              </select>
            </div>
          </div>

          <div class="mb-4">
            <label class="form-label fw-bold text-secondary">メモ</label>
            <textarea class="form-control" v-model="form.notes" rows="3" placeholder="特記事項など"></textarea>
          </div>

          <div class="d-flex justify-content-end gap-2 border-top pt-3">
            <BaseButton type="button" class="btn btn-outline-secondary px-4" @click="$router.push('/beetles')">キャンセル</BaseButton>
            <BaseButton type="submit" class="btn btn-success px-5 fw-bold" :disabled="isSubmitting">登録する</BaseButton>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
