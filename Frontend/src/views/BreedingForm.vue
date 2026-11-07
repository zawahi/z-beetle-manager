<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'

const router = useRouter()

const errorMessage = ref('')

const form = ref({
  maleName: '',
  femaleName: '',
  startDate: new Date().toISOString().split('T')[0],
  temperature: null,
  notes: ''
})

const isSubmitting = ref(false)

const submitForm = async () => {
  isSubmitting.value = true
  errorMessage.value = ''
  try {
    await axios.post('/api/BreedingRecords', form.value)
    router.push('/breeding')
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
    <h2 class="mb-4 text-secondary border-bottom pb-2">新規ペアリング開始</h2>
    
    <div v-if="errorMessage" class="alert alert-danger shadow-sm border-0 mb-4">
      {{ errorMessage }}
    </div>

    <div class="card shadow-sm border-0">
      <div class="card-body p-4">
        <form @submit.prevent="submitForm">
          
          <div class="row mb-3">
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">オス親の名前・管理番号</label>
              <BaseInput type="text" class="form-control" v-model="form.maleName" required placeholder="例: 能勢YG-01" />
            </div>
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">メス親の名前・管理番号</label>
              <BaseInput type="text" class="form-control" v-model="form.femaleName" required placeholder="例: 能勢YG-02" />
            </div>
          </div>

          <div class="row mb-4">
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">同居開始日</label>
              <BaseInput type="date" class="form-control" v-model="form.startDate" required />
            </div>
            <div class="col-md-6">
              <label class="form-label fw-bold text-secondary">管理温度 (℃)</label>
              <BaseInput type="number" step="0.1" class="form-control" v-model="form.temperature" placeholder="例: 24.5" />
            </div>
          </div>

          <div class="mb-4">
            <label class="form-label fw-bold text-secondary">メモ (セット内容など)</label>
            <textarea class="form-control" v-model="form.notes" rows="4" placeholder="例: コバシャ小、クヌギ材1本"></textarea>
          </div>

          <div class="d-flex justify-content-end gap-2 border-top pt-3">
            <BaseButton type="button" class="btn btn-outline-secondary px-4" @click="$router.push('/breeding')">キャンセル</BaseButton>
            <BaseButton type="submit" class="btn btn-success px-5 fw-bold" :disabled="isSubmitting">記録する</BaseButton>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
