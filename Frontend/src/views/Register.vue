<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'

const router = useRouter()
const form = ref({ email: '', password: '' })
const errorMsg = ref('')

const register = async () => {
  try {
    await axios.post('/api/Auth/register', form.value)
    alert('登録が完了しました。ログインしてください。')
    router.push('/login')
  } catch (error) {
    errorMsg.value = '登録に失敗しました。パスワードは6文字以上にしてください。'
  }
}
</script>

<template>
  <div class="container d-flex justify-content-center align-items-center vh-100 bg-light" style="max-width: 100%;">
    <div class="col-md-6 col-lg-4">
      <div class="card shadow-lg border-0 rounded-lg">
        <div class="card-header bg-dark text-white text-center py-4">
          <h3 class="mb-0">Kuwagata App</h3>
        </div>
        <div class="card-body p-4">
          <h5 class="text-center mb-4 text-muted">アカウントの新規登録</h5>
          
          <form @submit.prevent="register">
            <div class="mb-3">
              <label class="form-label text-secondary fw-bold">メールアドレス</label>
              <BaseInput type="email" class="form-control form-control-lg" v-model="form.email" placeholder="name@example.com" required />
            </div>
            
            <div class="mb-4">
              <label class="form-label text-secondary fw-bold">パスワード <span class="text-danger small">*6文字以上</span></label>
              <BaseInput type="password" class="form-control form-control-lg" v-model="form.password" placeholder="********" required minlength="6" />
            </div>

            <div v-if="errorMsg" class="alert alert-danger py-2" role="alert">
              {{ errorMsg }}
            </div>

            <div class="d-grid gap-2 mt-4">
              <BaseButton type="submit" class="btn btn-primary btn-lg fw-bold">無料で登録する</BaseButton>
            </div>
          </form>
        </div>
        <div class="card-footer text-center py-3 bg-white border-0">
          <div class="small text-muted">
            すでにアカウントをお持ちですか？ 
            <router-link to="/login" class="text-primary fw-bold text-decoration-none">ログイン画面へ</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  margin: 0 !important;
  padding: 0 !important;
}
</style>
