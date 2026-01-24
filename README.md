🌾 Agri Procurement Platform – DevOps Infrastructure

📌 Project Overview

This repository contains the DevOps infrastructure setup for the Agri Procurement Platform.
The focus of this project is deployment automation, GitOps, monitoring, and logging using modern cloud-native tools.

The application itself is a .NET 9 ASP.NET Core API, deployed and managed entirely through Kubernetes and GitOps practices.

🧱 System Architecture

![alt text](image.png)

⚙️ Technology Stack & Versions


🔹 Application
  
  .NET SDK	        [.NET 9]
  
  ASP.NET Core	    [9.0]
  
  Application Type	[REST API]


🔹 Container & Orchestration
  
  Docker	            [Latest]
  
  Kubernetes	        [v1.29+]
  
  Helm	              [v3.x]


🚀 Deployment Workflow

  1. Application is packaged using Helm
  
  2. Helm charts are stored in Git repository
  
  3. ArgoCD watches the repository for changes
  
  4. Kubernetes resources are synced automatically
  
  5. Application pods are deployed to the cluster
  
  6. Prometheus scrapes metrics from /metrics
  
  7. Grafana visualizes metrics and logs
  
  8. Loki collects logs via Promtail


📊 Monitoring (Prometheus & Grafana)

![alt text](image-1.png)

![alt text](image-2.png)

  Metrics Collected
  
  HTTP request duration
  
  Request count by status code
  
  CPU & memory usage
  
  .NET GC metrics
  
  Thread count
  
  Dashboards
  
  Kubernetes Cluster Overview
  
  Application Metrics Dashboard
  
  Node Exporter Metrics



📜 Logging (Loki & Promtail)

![alt text](image-3.png)

  Logs are collected from all application pods
  
  Centralized log storage using Loki
  
  Logs visualized in Grafana
  
  Supports:
  
  Namespace filtering
  
  Pod & container filtering
  
  Log level search (INFO / WARN / ERROR)


🔄 ArgoCD – GitOps

![alt text](image-4.png)

  ArgoCD is responsible for:
  
  Continuous synchronization between Git and cluster
  
  Declarative Kubernetes deployments
  
  Drift detection and self-healing
  
  Rollback using Git history
  
  Key Benefits:
  
  Single source of truth (Git)
  
  Automated deployments
  
  Safe and auditable changes

![alt text](image-5.png)

🧑‍💻 Author

Role: DevOps Engineer


Focus: Kubernetes, GitOps, Monitoring, Cloud-Native Infrastructure

📌 Notes

This project is intended for educational and demonstration purposes, showcasing real-world DevOps practices and tooling.
