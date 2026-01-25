# 📘 README.md — AgriProcurement Smart Contract

# AgriProcurement – Blockchain-based Agricultural Procurement System

## 📌 Project Overview

AgriProcurement is a blockchain-based smart contract designed to improve transparency, trust, and security in agricultural procurement processes. 

The system implements an **escrow-based payment mechanism**, ensuring that farmers and buyers can transact without relying on intermediaries.

Payments are locked in the smart contract and released only after inspection approval, reducing fraud, disputes, and delayed payments.

---

## 🎯 Problem Statement

Traditional agricultural procurement systems often suffer from:
- Lack of transparency
- Delayed payments to farmers
- Dependence on trusted third parties
- Risk of fraud and contract manipulation

This project addresses these issues by using **Ethereum smart contracts** to enforce business rules automatically and immutably.

---

## 🧠 Solution Overview

The smart contract acts as an **escrow**:
1. Buyer creates an order and deposits ETH
2. Funds are locked inside the smart contract
3. Inspector verifies delivery and quality
4. Payment is released to the seller, or refunded to the buyer if cancelled

All actions are recorded on the blockchain, ensuring full auditability.

---

## 🏗️ System Architecture

- **Blockchain**: Ethereum (Sepolia Test Network)
- **Smart Contract Language**: Solidity
- **Development Framework**: Hardhat
- **Testing**: Mocha + Chai
- **Coverage Tool**: solidity-coverage
- **Wallet**: MetaMask
- **Node Provider**: Alchemy

---

## 📂 Project Structure

```
agri-procurement/
├── contracts/
│   └── ProcurementOrder.sol
├── scripts/
│   └── deploy.js
├── test/
│   └── ProcurementOrder.test.js
├── hardhat.config.js
├── package.json
├── .env
└── README.md
```

---

## ⚙️ Smart Contract Features

- Create procurement orders
- Lock payments in escrow
- Inspection and approval mechanism
- Secure payment release
- Refund handling on cancellation
- Role-based access control

---

## 🧪 Testing

Unit tests were written to validate all critical contract logic.

### Test Coverage Results:
- **Statements**: 100%
- **Functions**: 100%
- **Lines**: 100%
- **Branches**: 94.44%

This high coverage demonstrates strong reliability and consideration of edge cases.

To run tests:
```bash
npx hardhat test
```

To generate coverage:
```bash
npx hardhat coverage
```

---

## 🚀 Deployment

The smart contract was successfully deployed to the Ethereum Sepolia Test Network.

Deployment was performed using:
```bash
npx hardhat run scripts/deploy.js --network sepolia
```

After deployment, the contract can be viewed and verified on Sepolia Etherscan using the generated contract address.

---

## 🔐 Security Considerations

- Immutable smart contract logic
- No centralized authority controlling funds
- Funds are only released based on predefined rules
- Environment variables used for sensitive credentials

---

## 🌱 Environmental Considerations

The project uses the Ethereum Proof-of-Stake (PoS) network, significantly reducing energy consumption compared to Proof-of-Work systems.

---

## 🔮 Future Improvements

- Integration with a frontend (React / Web3.js)
- Multi-inspector validation
- Support for stablecoins
- On-chain reputation system
- DAO-based dispute resolution

---

## 👨‍💻 Author

Developed as part of a Blockchain Systems and Applications assignment, demonstrating real-world use of smart contracts in the agricultural sector.

---

## 📜 License

This project is provided for educational purposes.
