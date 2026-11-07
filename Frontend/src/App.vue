<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import Sidebar from './components/Sidebar.vue'
import ConfirmModal from './components/ConfirmModal.vue'

const router = useRouter()
const route = useRoute()
const isLoggedIn = computed(() => !!localStorage.getItem('token'))

const showLogoutModal = ref(false)
const showMobileMenu = ref(false)

const executeLogout = () => {
  localStorage.removeItem('token')
  window.location.href = '/login'
}
</script>

<template>
  <!-- ログイン済みの場合のレイアウト -->
  <div class="d-flex flex-column flex-md-row vh-100 overflow-hidden" v-if="isLoggedIn">
    
    <!-- スマホ向けヘッダー (ハンバーガーメニュー) -->
    <div class="d-md-none bg-dark text-white p-3 d-flex justify-content-between align-items-center shadow-sm z-3">
      <h5 class="m-0 fw-bold">Kuwagata App</h5>
      <BaseButton class="btn btn-outline-light btn-sm" @click="showMobileMenu = !showMobileMenu">
        <span v-if="!showMobileMenu">☰</span>
        <span v-else>✕</span>
      </BaseButton>
    </div>

    <!-- サイドバー コンポーネント (PC用) -->
    <Sidebar class="d-none d-md-flex" :onLogout="() => showLogoutModal = true" />

    <!-- スマホ向けオフキャンバスメニュー -->
    <div v-if="showMobileMenu" class="d-md-none position-fixed top-0 start-0 w-100 h-100 z-2" style="background: rgba(0,0,0,0.5); padding-top: 60px;" @click="showMobileMenu = false">
      <div class="h-100" @click.stop>
        <Sidebar class="h-100 w-75 shadow-lg" :onLogout="() => showLogoutModal = true" @nav-clicked="showMobileMenu = false" />
      </div>
    </div>

    <!-- メインコンテンツ -->
    <div class="flex-grow-1 p-3 p-md-4 bg-light overflow-auto">
      <div class="container-fluid p-0 p-md-2" style="max-width: 1200px;">
        <router-view />
      </div>
    </div>

    <!-- ログアウト確認モーダル コンポーネント -->
    <ConfirmModal 
      :show="showLogoutModal" 
      title="ログアウト" 
      message="ログアウトしてもよろしいですか？" 
      confirmText="ログアウトする"
      confirmVariant="danger"
      @close="showLogoutModal = false" 
      @confirm="executeLogout" 
    />
  </div>

  <!-- 未ログイン（ログイン画面など）の場合 -->
  <div v-else>
    <router-view />
  </div>
</template>

<style>
/* Active state customization for sidebar */
.nav-pills .nav-link.active {
  background-color: #198754; /* Bootstrap Success Color */
}
</style>
