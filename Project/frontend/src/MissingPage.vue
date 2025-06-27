<template>
  <div class="not-found-page">
    <h1>404 - Página Não Encontrada</h1>
    <p>Oops! A página que procuras não existe.</p>
    <button @click="goHome">Voltar à página inicial</button>
  </div>
</template>

<script>
export default {
  name: "NotFoundPage",
  methods: {
    goHome() {
      this.$router.push('/')
    }
  }
}
</script>

<style scoped>
.not-found-page {
  max-width: 500px;
  margin: 100px auto;
  text-align: center;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  color: #e74c3c;
}

h1 {
  font-size: 3em;
  margin-bottom: 0.5em;
}

p {
  font-size: 1.2em;
  margin-bottom: 2em;
}

button {
  background-color: #3498db;
  color: white;
  border: none;
  padding: 12px 30px;
  border-radius: 8px;
  font-size: 1.1em;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: #2980b9;
}
</style>
