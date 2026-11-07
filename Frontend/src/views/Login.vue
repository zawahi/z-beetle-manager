<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'

const router = useRouter()
const form = ref({ email: '', password: '' })
const errorMsg = ref('')

const login = async () => {
  try {
    const res = await axios.post('/api/Auth/login', form.value)
    localStorage.setItem('token', res.data.token)
    window.location.href = '/'
  } catch (error) {
    errorMsg.value = 'ログインに失敗しました。メールアドレスとパスワードを確認してください。'
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
          <h5 class="text-center mb-4 text-muted">ログイン</h5>
          
          <form @submit.prevent="login">
            <div class="mb-3">
              <label class="form-label text-secondary fw-bold">メールアドレス</label>
              <BaseInput type="email" class="form-control form-control-lg" v-model="form.email" placeholder="name@example.com" required />
            </div>
            
            <div class="mb-4">
              <label class="form-label text-secondary fw-bold">パスワード</label>
              <BaseInput type="password" class="form-control form-control-lg" v-model="form.password" placeholder="********" required />
            </div>

            <div v-if="errorMsg" class="alert alert-danger py-2" role="alert">
              {{ errorMsg }}
            </div>

            <div class="d-grid gap-2 mt-4">
              <BaseButton type="submit" class="btn btn-success btn-lg fw-bold">ログイン</BaseButton>
            </div>
          </form>
        </div>
        <div class="card-footer text-center py-3 bg-white border-0">
          <div class="small text-muted">
            アカウントをお持ちでないですか？ 
            <router-link to="/register" class="text-success fw-bold text-decoration-none">新規登録はこちら</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Reset any global styles interfering with this specific full-page layout */
.container {
  margin: 0 !important;
  padding: 0 !important;
}
</style>
