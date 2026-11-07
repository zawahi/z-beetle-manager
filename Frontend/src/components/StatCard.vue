<script setup lang="ts">
import { useRouter } from 'vue-router'
const router = useRouter()

defineProps({
  title: String,
  value: [String, Number],
  link: String,
  borderColorClass: { type: String, default: 'border-primary' }
})
</script>

<template>
  <div class="col-md-3 col-sm-6">
    <div class="card shadow-sm border-0 border-top border-4 h-100 text-center clickable-card" :class="borderColorClass" @click="router.push(link)">
      <div class="card-body">
        <h6 class="text-muted text-uppercase mb-2">{{ title }}</h6>
        <h2 class="display-5 fw-bold text-dark mb-0">{{ value }}</h2>
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
