"use client"
import "../professions.page.css";

import { useEffect, useState } from "react";

import { Loading } from "../../shared/ui/Loading";
import {  getAllProfessionsWithSlug } from "../../shared/api/professions";

import { ProfessionsSkillsCards } from "../ui/ProfessionsSkillsCards";
import { Content } from "antd/es/layout/layout";


export default function ProfessionsPage() {
  const [professions, setProfessions] = useState<ProfessionWithSlug[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getProfessions = async () => {
        const professions = await getAllProfessionsWithSlug();

        setLoading(false);
        setProfessions(professions);
      };

    getProfessions();
  }, []);

  return (
    <Content>
      {loading ? (
        <Loading></Loading>
      ) : (
        <ProfessionsSkillsCards professions={professions} />
      )}
    </Content>
  );
}


