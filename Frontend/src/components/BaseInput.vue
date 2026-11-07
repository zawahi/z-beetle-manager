<script setup lang="ts">
const model = defineModel()

defineProps({
  type: { type: String, default: 'text' },
  placeholder: { type: String, default: '' },
  required: { type: Boolean, default: false },
  step: { type: [String, Number], default: null },
  minlength: { type: [String, Number], default: null },
  disabled: { type: Boolean, default: false }
})
</script>

<template>
  <input 
    :type="type" 
    v-model="model"
    :placeholder="placeholder"
    :required="required"
    :step="step"
    :minlength="minlength"
    :disabled="disabled"
  />
</template>
