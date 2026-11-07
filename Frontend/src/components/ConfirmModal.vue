<script setup lang="ts">
defineProps({
  show: Boolean,
  title: String,
  message: String,
  confirmText: { type: String, default: '確認' },
  confirmVariant: { type: String, default: 'danger' }
})

const emit = defineEmits(['close', 'confirm'])
</script>

<template>
  <div v-if="show" class="modal fade show d-block" style="background: rgba(0,0,0,0.5); z-index: 1050;" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content border-0 shadow">
        <div class="modal-header border-bottom-0">
          <h5 class="modal-title fw-bold" :class="`text-${confirmVariant}`">{{ title }}</h5>
          <BaseButton type="button" class="btn-close" @click="emit('close')"></BaseButton>
        </div>
        <div class="modal-body text-secondary">
          {{ message }}
        </div>
        <div class="modal-footer border-top-0">
          <BaseButton type="button" class="btn btn-light px-4" @click="emit('close')">キャンセル</BaseButton>
          <BaseButton type="button" class="btn px-4 fw-bold" :class="`btn-${confirmVariant}`" @click="emit('confirm')">{{ confirmText }}</BaseButton>
        </div>
      </div>
    </div>
  </div>
</template>
