<template>
  <div class="home-page">
    <h1>HeavenBooking</h1>
    <p class="author">
      Autor: André Campos — 
      <a href="https://github.com/afbc04/HeavenBooking" target="_blank" rel="noopener noreferrer">
        GitHub /afbc04/HeavenBooking
      </a>
    </p>

    <div class="buttons-container">
      <router-link to="/users" class="nav-button">Utilizadores</router-link>
      <router-link to="/hotels" class="nav-button">Hoteis</router-link>
      <router-link to="/reservations" class="nav-button">Reservas</router-link>
      <router-link to="/flights" class="nav-button">Voos</router-link>
      <router-link to="/airports" class="nav-button">Aeroportos</router-link>
      <router-link to="/metrics" class="nav-button">Métricas</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "HomePage",
};
</script>

<style scoped>
.home-page {
  max-width: 600px;
  margin: 60px auto;
  padding: 30px;
  text-align: center;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  color: #2c3e50;
  background: linear-gradient(145deg, #f0f4f8, #d9e2ec);
  border-radius: 16px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.1);
}

h1 {
  font-size: 3em;
  margin-bottom: 0.3em;
  color: #34495e;
}

.author {
  font-size: 1.1em;
  margin-bottom: 2.5em;
  color: #555;
}

.author a {
  color: #3498db;
  text-decoration: none;
  font-weight: 600;
}
.author a:hover {
  text-decoration: underline;
}

.buttons-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 15px;
}

.nav-button {
  background-color: #3498db;
  color: white;
  text-decoration: none;
  padding: 14px 24px;
  border-radius: 10px;
  font-size: 1.1em;
  font-weight: bold;
  transition: background-color 0.3s ease;
  min-width: 140px;
  display: inline-block;
}

.nav-button:hover {
  background-color: #2980b9;
}
</style>
