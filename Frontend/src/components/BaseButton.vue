<script setup lang="ts">
defineProps({
  type: {
    type: String as () => 'button' | 'submit' | 'reset',
    default: 'button'
  },
  disabled: {
    type: Boolean,
    default: false
  }
})
</script>

<template>
  <button 
    :type="type" 
    :disabled="disabled"
  >
    <slot></slot>
  </button>
</template>
