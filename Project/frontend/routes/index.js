const express = require('express');
const router = express.Router();
const path = require('path');

// Para qualquer rota, retorna o index.html (importante para apps SPA)
router.get('*', (req, res) => {
  res.sendFile(path.join(__dirname, 'dist', 'index.html'));
});

module.exports = router;
