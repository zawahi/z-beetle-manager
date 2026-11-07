<script setup lang="ts">
import { useRoute } from 'vue-router'
const route = useRoute()

defineProps({
  onLogout: {
    type: Function,
    required: true
  }
})

const emit = defineEmits(['nav-clicked'])
</script>

<template>
  <div class="bg-dark text-white p-3 d-flex flex-column" style="width: 260px; max-width: 100%;">
    <div class="mb-4 mt-2 text-center d-none d-md-block">
      <h4 class="fw-bold m-0">Kuwagata App</h4>
    </div>
    <hr class="text-secondary d-none d-md-block">
    <ul class="nav nav-pills flex-column mb-auto gap-2">
      <li class="nav-item">
        <router-link to="/" class="nav-link text-white" :class="{ 'active': route.path === '/' }" @click="emit('nav-clicked')">
          ダッシュボード
        </router-link>
      </li>
      <li class="nav-item">
        <router-link to="/beetles" class="nav-link text-white" :class="{ 'active': route.path.startsWith('/beetle') }" @click="emit('nav-clicked')">
          生体・幼虫管理
        </router-link>
      </li>
      <li class="nav-item">
        <router-link to="/breeding" class="nav-link text-white" :class="{ 'active': route.path.startsWith('/breeding') }" @click="emit('nav-clicked')">
          ペアリング記録
        </router-link>
      </li>
    </ul>
    <hr class="text-secondary">
    <BaseButton class="btn btn-outline-light w-100 fw-bold" @click="onLogout">ログアウト</BaseButton>
  </div>
</template>
